public class ShadowStateMachine : StateMachine
{
    public IState HitState { get; private set; }
    public IState BoundState { get; private set; }

    public ShadowStateMachine(Shadow shadow)
    {
        HitState = new ShadowHitState(shadow, this);
        BoundState = new ShadowBoundState(shadow, this);
    }
}
