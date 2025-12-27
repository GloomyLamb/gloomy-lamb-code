using System;
using UnityEngine;

public abstract class Shadow : MonoBehaviour, IAttackable, IDamageable
{
    #region 필드
    // controller
    protected ShadowController controller;
    public Transform Target => controller.Target;
    public Vector3 Forward => controller.transform.forward;

    // state machine
    protected ShadowStateMachine stateMachine;

    [Header("애니메이션")]
    [field: SerializeField] public Animator Animator;
    [field: SerializeField] public ShadowAnimationData AnimationData { get; protected set; }
    public float TransformDuration => controller.TransformDuration;
    public float HitDuration => controller.HitDuration;
    public float BoundStopPoint => controller.BoundStopPoint;
    public float BoundDuration => controller.BoundDuration;

    [field: Header("움직임")]
    [field: SerializeField] public MoveStatusData MoveStatusData { get; protected set; }
    public float MovementSpeed => MoveStatusData.MoveSpeed;
    private float _movementSpeedMultiplier = 1f;
    protected float MovementSpeedMultiplier
    {
        get { return _movementSpeedMultiplier; }
        private set
        {
            _movementSpeedMultiplier = value;
            if (controller != null)
            {
                controller.SetAgentMovementModifier(_movementSpeedMultiplier);
            }
        }
    }
    [SerializeField] protected float damage = 10f;

    [field: Header("효과")]
    [field: SerializeField] public float FogScaleModifier { get; private set; } = 2f;

    // 이벤트
    public Action OnMove;               // 이동
    public event Action OnTransform;    // 변형
    #endregion

    #region 초기화
    protected virtual void Awake()
    {
        AnimationData.Initialize();
    }

    protected virtual void Start()
    {
    }

    public virtual void Init(ShadowController controller)
    {
        this.controller = controller;
    }

    protected virtual void OnEnable()
    {
        stateMachine.Register();
        stateMachine.ChangeState(stateMachine.IdleState);
    }
    #endregion

    protected virtual void Update()
    {
        if (CanTransform())
        {
            ResetTransformFlag();
            stateMachine.ChangeState(stateMachine.TransformState);
            return;
        }

        stateMachine?.Update();
    }

    protected virtual void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }

    protected virtual void OnDisable()
    {
        stateMachine.UnRegister();
    }

    // IDamageable
    public virtual void ApplyEffect()
    {
    }

    public virtual void StopEffect()
    {

    }

    public virtual void Damage(float damage)
    {
        Logger.Log($"데미지: {damage}");
        if (stateMachine == null)
        {
            Logger.Log("state machine 없음");
            return;
        }
        stateMachine.ChangeState(stateMachine.HitState);
        controller.Damage(damage);
    }

    // IAttackable
    public virtual void Attack()
    {
    }

    public virtual void GiveEffect()
    {
    }

    #region 움직임
    public virtual void SetMovementMultiplier(MovementType type)
    {
        MovementSpeedMultiplier = (type) switch
        {
            MovementType.Stop => 0f,
            MovementType.Walk => 1f,
            MovementType.Run => MoveStatusData.DashMultiplier,
            _ => 1f,
        };
    }
    #endregion

    #region 바인딩
    public virtual void Bound()
    {
        Logger.Log("바인딩");
        stateMachine?.ChangeState(stateMachine.BoundState);
    }
    #endregion

    #region 변형
    public void Transform()
    {
        OnTransform?.Invoke();
    }

    /// <summary>
    /// 변환 조건 확인하기
    /// </summary>
    /// <returns></returns>
    protected abstract bool CanTransform();

    /// <summary>
    /// 변환 조건 초기화하기
    /// </summary>
    protected abstract void ResetTransformFlag();
    #endregion

    #region 에디터 전용
#if UNITY_EDITOR
    protected virtual void Reset()
    {
        Animator = GetComponentInChildren<Animator>();
    }
#endif
    #endregion
}
