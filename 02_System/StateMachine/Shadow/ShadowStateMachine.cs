public class ShadowStateMachine : StateMachine
{
    public IState IdleState { get; protected set; }
    public IState ChangeState { get; private set; }

    public IState HitState { get; private set; }
    public IState BoundState { get; private set; }

    public ShadowStateMachine(Shadow shadow)
    {
        ChangeState = new ShadowChangeState(shadow, this);

        HitState = new ShadowHitState(shadow, this);
        BoundState = new ShadowBoundState(shadow, this);
    }
}
