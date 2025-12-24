public class ShadowStateMachine : StateMachine
{
    public Shadow Shadow { get; private set; }

    public CommonShadowState IdleState { get; protected set; }
    public CommonShadowState ChaseState { get; protected set; }
    public CommonShadowState TransformState { get; private set; }

    public CommonShadowState HitState { get; private set; }
    public CommonShadowState BoundState { get; private set; }

    public ShadowStateMachine(Shadow shadow)
    {
        Shadow = shadow;

        IdleState = new CommonShadowState(shadow, this);
        ChaseState = new CommonShadowState(shadow, this);
        TransformState = new ShadowTransformState(shadow, this);

        HitState = new ShadowHitState(shadow, this);
        BoundState = new ShadowBoundState(shadow, this);

    }

    public virtual void Init()
    {
        IdleState.Init(MovementType.Stop, Shadow.AnimationData.IdleParameterHash, AnimType.Bool);
        ChaseState.Init(MovementType.Default, Shadow.AnimationData.ChaseParameterHash, AnimType.Bool);
        TransformState.Init(MovementType.Stop, Shadow.AnimationData.TransformParameterHash, AnimType.Bool, true);

        HitState.Init(MovementType.Stop, Shadow.AnimationData.HitParameterHash, AnimType.Trigger);
        BoundState.Init(MovementType.Stop, Shadow.AnimationData.BoundParameterHash, AnimType.Bool, true);

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
        if (Shadow.Target != null)
        {
            ChangeState(ChaseState);
        }
    }

    protected virtual void HandleUpdateChase()
    {
        if (Shadow.Target == null)
        {
            ChangeState(IdleState);
        }
    }
    #endregion

    protected virtual void HandleFixedUpdateChase()
    {
        Shadow.OnMove?.Invoke();
    }
}
