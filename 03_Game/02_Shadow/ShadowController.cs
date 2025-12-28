using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class ShadowController : MonoBehaviour
{
    #region 필드
    // 컴포넌트
    private NavMeshAgent _agent;
    public NavMeshAgent Agent => _agent;

    // 현재 그림자
    protected Shadow curShadow;

    [Header("스탯 SO")]
    [SerializeField] protected StatusData statusData;
    public Status Status => status;
    protected Status status;
    [field: SerializeField] public float BoundDamageMultiplier = 1.2f;

    [field: Header("효과")]
    [field: SerializeField] public Transform ShadowFog { get; private set; }
    protected float scaleDuration = 0.5f;
    private Vector3 _defaultFogScale;
    private Coroutine _scaleCoroutine;

    [field: Header("추격")]
    [field: SerializeField] public Transform Target { get; private set; }
    [SerializeField] private float _updateInterval = 0.1f;
    private float _agentTimer;

    [field: Header("시간 설정")]
    [field: SerializeField] public float TransformDuration = 2f;
    [field: SerializeField] public float HitDuration { get; protected set; } = 1f;
    [field: SerializeField] public float BoundDuration { get; protected set; } = 2f;
    [field: SerializeField] public float BoundStopPoint { get; protected set; } = 0.1f;

    #endregion

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        status = statusData?.GetNewStatus();

        _defaultFogScale = ShadowFog.localScale;
    }

    protected virtual void Start()
    {
        if (Target == null)
        {
            Target = GameManager.Instance.Player.transform;
        }
    }

    public void Damage(float damage)
    {
        Status.AddHp(-damage);
    }

    protected void TransformFogScaleTo()
    {
        if (_scaleCoroutine != null)
        {
            StopCoroutine(_scaleCoroutine);
            _scaleCoroutine = null;
        }
        _scaleCoroutine = StartCoroutine(ScaleToCoroutine(
            ShadowFog,
            _defaultFogScale,
            curShadow.FogScaleModifier,
            scaleDuration));
    }

    public void TransformFogScaleTo(float modifier)
    {
        if (_scaleCoroutine != null)
        {
            StopCoroutine(_scaleCoroutine);
            _scaleCoroutine = null;
        }
        _scaleCoroutine = StartCoroutine(ScaleToCoroutine(
            ShadowFog,
            _defaultFogScale,
            modifier,
            scaleDuration));
    }

    protected IEnumerator ScaleToCoroutine(Transform target, Vector3 startScale, float scaleModifier, float duration)
    {
        Vector3 endScale = startScale * scaleModifier;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            target.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.localScale = endScale;
    }

    #region Nev Mesh Agent 관리
    public void SetActiveAgent(bool active)
    {
        _agent.isStopped = !active;
        _agent.updatePosition = active;
        _agent.updateRotation = active;
    }

    public void SetActiveAgentRotation(bool active)
    {
        _agent.updateRotation = active;
    }

    protected void HandleMove()
    {
        if (Target == null)
        {
            //Logger.Log("타겟 없음");
            _agent.ResetPath();
        }

        _agentTimer += Time.deltaTime;
        if (_agentTimer > _updateInterval)
        {
            Vector3 targetPosition = Target.position + (new Vector3(1f, 0, 1f) * Random.Range(0.5f, 0.75f));
            _agent.SetDestination(targetPosition);
            _agentTimer = 0f;
        }
    }

    public void SetAgentMovementModifier(float modifier)
    {
        //Logger.Log("nev agent 속도 변경");
        _agent.speed = curShadow.MovementSpeed * modifier;
    }

    private NavMeshAgent GetAgentSafely()
    {
        var shadowObj = Object.FindObjectOfType<Shadow>();
        if (shadowObj != null)
            return shadowObj.GetComponent<NavMeshAgent>();

        return null;
    }
    #endregion

    #region 에디터 전용
#if UNITY_EDITOR
    protected virtual void Reset()
    {
        ShadowFog = transform.FindChild<Transform>("Particle_Fog");
    }
#endif
    #endregion
}
