public class ShadowStateMachine : StateMachine
{
    public Shadow Shadow { get; private set; }

    public ShadowState IdleState { get; protected set; }
    public ShadowState ChaseState { get; protected set; }
    public ShadowState TransformState { get; private set; }

    public ShadowState HitState { get; private set; }
    public ShadowState BoundState { get; private set; }

    /// <summary>
    /// 생성자 : 각종 State를 생성
    /// </summary>
    /// <param name="shadow"></param>
    public ShadowStateMachine(Shadow shadow)
    {
        Shadow = shadow;

        IdleState = new ShadowState(shadow, this);
        ChaseState = new ShadowState(shadow, this);
        TransformState = new ShadowTransformState(shadow, this);

        HitState = new ShadowHitState(shadow, this);
        BoundState = new ShadowBoundState(shadow, this);

    }

    /// <summary>
    /// State에 각종 정보 전달
    /// </summary>
    public virtual void Init()
    {
        IdleState.Init(MovementType.Stop, Shadow.AnimationData.IdleParameterHash, AnimType.Bool);
        ChaseState.Init(MovementType.Default, Shadow.AnimationData.ChaseParameterHash, AnimType.Bool);
        TransformState.Init(MovementType.Stop, Shadow.AnimationData.TransformParameterHash, AnimType.Bool, true);

        HitState.Init(MovementType.Stop, Shadow.AnimationData.HitParameterHash, AnimType.Trigger);
        BoundState.Init(MovementType.Stop, Shadow.AnimationData.BoundParameterHash, AnimType.Bool, true);

        Publish();
    }

    /// <summary>
    /// State에 이벤트를 구독합니다.
    /// 현재 구독할 수 있는 범위 - Update, FixedUpdate
    /// </summary>
    protected virtual void Publish()
    {
        // Update
        IdleState.OnUpdate += HandleUpdateIdle;
        ChaseState.OnUpdate += HandleUpdateChase;

        // FixedUpdate
        ChaseState.OnFixedUpdate += HandleFixedUpdateChase;
    }

    #region 상태 Update 내부 로직
    /// <summary>
    /// 기본 상태 Update
    /// </summary>
    protected virtual void HandleUpdateIdle()
    {
        if (Shadow.Target != null)
        {
            ChangeState(ChaseState);
        }
    }

    /// <summary>
    /// 추적 상태 Update
    /// </summary>
    protected virtual void HandleUpdateChase()
    {
        if (Shadow.Target == null)
        {
            ChangeState(IdleState);
        }
    }
    #endregion

    /// <summary>
    /// 추적 상태 FixedUpdate
    /// </summary>
    protected virtual void HandleFixedUpdateChase()
    {
        Shadow.OnMove?.Invoke();
    }
}
