using UnityEngine;
using UnityEngine.AI;

public abstract class ShadowController : MonoBehaviour
{
    // todo: 각종 스텟
    [Header("스탯 SO")]
    [SerializeField] protected StatusData statusData;
    public Status Status => status;
    protected Status status;

    protected Shadow curShadow;

    [field: Header("추격")]
    [SerializeField] private NavMeshAgent _agent;
    [field: SerializeField] public Transform Target { get; private set; }

    private float _agentTimer;
    [SerializeField] private float _updateInterval = 0.1f;

    [field: Header("시간 설정")]
    [field: SerializeField] public float TransformDuration = 2f;
    [field: SerializeField] public float HitDuration { get; protected set; } = 1f;
    [field: SerializeField] public float BoundDuration;
    [field: SerializeField] public float BoundStopPoint;

    private Shadow shadow;
    
    protected virtual void Awake()
    {
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
        _agent = GetComponent<NavMeshAgent>();
    }
#endif
    #endregion
}
