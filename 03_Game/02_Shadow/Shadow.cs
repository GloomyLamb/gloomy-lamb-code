using System;
using UnityEngine;

public enum MovementType
{
    Stop,
    Default,
    Run,
}

public abstract class Shadow : MonoBehaviour, IAttackable, IDamageable
{
    // controller
    private ShadowController _controller;
    public Transform Target => _controller.Target;

    // state machine
    protected ShadowStateMachine stateMachine;

    [Header("애니메이션")]
    [field: SerializeField] public Animator Animator;
    [field: SerializeField] public ShadowAnimationData AnimationData { get; protected set; }
    public float HitDuration => _controller.HitDuration;
    public WaitForSeconds BoundStopPoint => _controller.BoundStopPoint;
    public WaitForSeconds BoundDuration => _controller.BoundDuration;

    // todo: 추후 SO로 분리
    [field: Header("움직임")]
    [field: SerializeField] public float MovementSpeed { get; set; } = 10f;
    [SerializeField] private float _defaultSpeedModifier = 1f;
    [SerializeField] private float _runSpeedModifier = 2f;
    private float _movementSpeedModifier = 1f;

    public float MovementSpeedModitier
    {
        get { return _movementSpeedModifier; }
        private set
        {
            Logger.Log("속도 보정값 변경");
            _movementSpeedModifier = value;
            if (_controller != null)
            {
                _controller.SetAgentMovementModifier(_movementSpeedModifier);
            }
        }
    }
    protected float rotatingDamping = 60f;
    [SerializeField] protected float damage = 10f;

    // todo: 타겟팅, 이동 통합
    // 움직임 이벤트
    public Action OnMove;

    // 변형
    public event Action OnTransform;

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
        _controller = controller;
    }

    protected virtual void OnEnable()
    {
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
        stateMachine?.PhysicsUpdate();
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
    }

    // IAttackable
    public virtual void Attack()
    {
    }

    public virtual void GiveEffect()
    {
    }

    #region 움직임
    public virtual void SetMovementModifier(MovementType type)
    {
        MovementSpeedModitier = (type) switch
        {
            MovementType.Stop => 0f,
            MovementType.Default => _defaultSpeedModifier,
            MovementType.Run => _runSpeedModifier,
            _ => _defaultSpeedModifier,
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
    private void Reset()
    {
        Animator = transform.FindChild<Animator>("Model");
    }
#endif
    #endregion
}
