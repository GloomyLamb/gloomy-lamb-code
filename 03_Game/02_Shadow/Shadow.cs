using System;
using System.Collections;
using UnityEngine;

public abstract class Shadow : MonoBehaviour, IAttackable, IDamageable
{
    [Header("애니메이션")]
    [field: SerializeField] public Animator Animator;
    [field: SerializeField] public ShadowAnimationData CommonAnimationData { get; protected set; }
    protected ShadowStateMachine stateMachine;

    private ShadowController _controller;
    public Transform Target => _controller.Target;

    // todo: 추후 SO로 분리
    [field: SerializeField] public float MovementSpeed { get; set; } = 10f;
    [SerializeField] private float _movementSpeedModifier = 1f;
    public float MovementSpeedModitier
    {
        get { return _movementSpeedModifier; }
        set
        {
            if (value != _movementSpeedModifier)
            {
                Logger.Log("그림자 속도 변경");
                _movementSpeedModifier = value;
                _controller.SetAgentMovementModifier(_movementSpeedModifier);
            }
        }
    }
    protected float rotatingDamping = 60f;
    [SerializeField] protected float damage = 10f;

    // todo: 타겟팅, 이동 통합
    // 움직임 이벤트
    public Action OnMove;

    // 바인딩
    [Header("바인딩")]
    [SerializeField] private float _stopPoint = 0.1f;
    [SerializeField] private float _boundTime = 3f;

    private Coroutine _boundCoroutine;
    private WaitForSeconds _stopDuration;
    private WaitForSeconds _boundDuration;

    // 변형
    public event Action OnChange;

    #region 초기화
    protected virtual void Awake()
    {
        CommonAnimationData.Initialize();

        _stopDuration = new WaitForSeconds(_stopPoint);
        _boundDuration = new WaitForSeconds(_boundTime);
    }

    protected virtual void Start()
    {
    }

    public virtual void Init(ShadowController controller)
    {
        _controller = controller;
    }
    #endregion

    protected virtual void Update()
    {
        if (CanTransform())
        {
            ResetTransformFlag();
            stateMachine.ChangeState(stateMachine.ChangeState);
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

    public virtual void Damage(float damage)
    {
        stateMachine?.ChangeState(stateMachine.HitState);
    }

    // IAttackable
    public virtual void Attack()
    {
    }

    public virtual void GiveEffect()
    {
    }

    #region 바인딩
    public virtual void Bound()
    {
        stateMachine?.ChangeState(stateMachine.BoundState);

        if (_boundCoroutine != null)
        {
            CustomCoroutineRunner.Instance.StopCoroutine(_boundCoroutine);
            _boundCoroutine = null;
        }
        _boundCoroutine = CustomCoroutineRunner.Instance.StartCoroutine(Binding());
    }

    private IEnumerator Binding()
    {
        yield return _stopDuration;
        Animator.speed = 0f;
        yield return _boundDuration;
        Animator.speed = 1f;
        stateMachine.ChangeState(stateMachine.IdleState);
    }
    #endregion

    #region 변형
    public void Change()
    {
        OnChange?.Invoke();
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
