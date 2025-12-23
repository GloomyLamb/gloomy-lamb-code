using System;
using System.Collections;
using UnityEngine;

public abstract class Shadow : MonoBehaviour, IAttackable, IDamageable
{
    [Header("애니메이션")]
    [field: SerializeField] public Animator Animator;
    [field: SerializeField] public ShadowAnimationData CommonAnimationData { get; protected set; }

    protected ShadowStateMachine stateMachine;

    // todo: 추후 SO로 분리
    [field: SerializeField] public float MovementSpeed { get; set; } = 10f;
    [field: SerializeField] public float MovementSpeedModitier { get; set; } = 1f;
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
    private WaitForSeconds _stopTimer;
    private WaitForSeconds _boundTimer;

    // 변형
    private bool _canTransform;

    protected virtual void Awake()
    {
        CommonAnimationData.Initialize();

        _stopTimer = new WaitForSeconds(_stopPoint);
        _boundTimer = new WaitForSeconds(_boundTime);
    }

    protected virtual void Update()
    {
        if (_canTransform)
        {
            Transform();
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
        yield return _stopTimer;
        Animator.speed = 0f;
        yield return _boundTimer;
        Animator.speed = 1f;
        stateMachine.ChangeState(stateMachine.IdleState);
    }
    #endregion

    #region 변형
    public void Transform()
    {
        _canTransform = false;
        stateMachine.ChangeState(stateMachine.TransformState);
    }
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
