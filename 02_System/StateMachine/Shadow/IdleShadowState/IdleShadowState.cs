public class IdleShadowState : BaseShadowState
{
    protected IdleShadowStateMachine StateMachine { get; private set; }

    public IdleShadowState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
        StateMachine = stateMachine as IdleShadowStateMachine;
    }
}
