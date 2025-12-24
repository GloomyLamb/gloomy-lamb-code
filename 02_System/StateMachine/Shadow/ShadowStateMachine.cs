public class ShadowStateMachine : StateMachine
{
    public Shadow shadow { get; private set; }

    public CommonShadowState IdleState { get; protected set; }
    public CommonShadowState ChaseState { get; protected set; }
    public CommonShadowState TransformState { get; private set; }

    public CommonShadowState HitState { get; private set; }
    public CommonShadowState BoundState { get; private set; }

    public ShadowStateMachine(Shadow shadow)
    {
        this.shadow = shadow;

        IdleState = new ShadowIdleState(shadow, this);
        ChaseState = new ShadowChaseState(shadow, this);
        TransformState = new ShadowTransformState(shadow, this);

        HitState = new ShadowHitState(shadow, this);
        BoundState = new ShadowBoundState(shadow, this);

    }

    public virtual void Init()
    {
        IdleState.Init(MovementType.Stop, shadow.AnimationData.IdleParameterHash, AnimType.Bool);
        ChaseState.Init(MovementType.Default, shadow.AnimationData.ChaseParameterHash, AnimType.Bool);
        TransformState.Init(MovementType.Stop, shadow.AnimationData.TransformParameterHash, AnimType.Bool, true);

        HitState.Init(MovementType.Stop, shadow.AnimationData.HitParameterHash, AnimType.Trigger);
        BoundState.Init(MovementType.Stop, shadow.AnimationData.BoundParameterHash, AnimType.Bool, true);

        Publish();
    }

    protected virtual void Publish()
    {
        // Update
        IdleState.OnUpdate += HandleUpdateIdle;
        ChaseState.OnUpdate += HandleUpdateChase;

        // FixedUpdate
        ChaseState.OnFixedUpdate += HandleFixedUpdateChase;
    }

    #region 상태 Update 내부 로직
    protected virtual void HandleUpdateIdle()
    {
        if (shadow.Target != null)
        {
            ChangeState(ChaseState);
        }
    }

    protected virtual void HandleUpdateChase()
    {
        if (shadow.Target == null)
        {
            ChangeState(IdleState);
        }
    }
    #endregion

    protected virtual void HandleFixedUpdateChase()
    {
        shadow.OnMove?.Invoke();
    }
}
