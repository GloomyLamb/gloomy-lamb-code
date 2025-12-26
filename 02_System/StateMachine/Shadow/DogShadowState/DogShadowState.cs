public class DogShadowState : ShadowState
{
    protected new DogShadow shadow;
    protected new DogShadowStateMachine StateMachine { get; private set; }

    public DogShadowState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
        this.shadow = shadow as DogShadow;
        StateMachine = stateMachine as DogShadowStateMachine;
    }
}
