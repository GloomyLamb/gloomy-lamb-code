public class DogShadowState : BaseShadowState
{
    protected DogShadowStateMachine StateMachine { get; set; }

    public DogShadowState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
        StateMachine = stateMachine as DogShadowStateMachine;
    }
}
