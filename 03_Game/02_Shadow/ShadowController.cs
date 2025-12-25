using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public abstract class ShadowController : MonoBehaviour
{
    #region 필드
    // 컴포넌트
    private Rigidbody _rigidbody;
    public Rigidbody Rigidbody => _rigidbody;

    private NavMeshAgent _agent;

    // todo: 각종 스텟
    [Header("스탯 SO")]
    [SerializeField] protected StatusData statusData;
    public Status Status => status;
    protected Status status;

    protected Shadow curShadow;

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
        _rigidbody = GetComponent<Rigidbody>();
        _agent = GetComponent<NavMeshAgent>();

        status = statusData?.GetNewStatus();
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

    #region 추격
    public void StopNevMeshAgent()
    {
        _agent.isStopped = true;
        _agent.updatePosition = false;
        _agent.updateRotation = false;
    }

    public void StartNevMeshAgent()
    {
        _agent.updatePosition = true;
        _agent.updateRotation = true;
        _agent.isStopped = false;
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
            _agent.SetDestination(Target.position);
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
    }
#endif
    #endregion
}
