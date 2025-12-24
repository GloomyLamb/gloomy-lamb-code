public class ShadowStateMachine : StateMachine
{
    public IState IdleState { get; protected set; }
    public IState ChaseState { get; protected set; }
    public IState TransformState { get; private set; }

    public IState HitState { get; private set; }
    public IState BoundState { get; private set; }

    public ShadowStateMachine(Shadow shadow)
    {
        IdleState = new ShadowIdleState(shadow, this);
        ChaseState = new ShadowChaseState(shadow, this);
        TransformState = new ShadowTransformState(shadow, this);

        HitState = new ShadowHitState(shadow, this);
        BoundState = new ShadowBoundState(shadow, this);
    }
}
