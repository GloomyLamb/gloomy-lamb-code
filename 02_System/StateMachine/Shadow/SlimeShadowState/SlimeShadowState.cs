public class SlimeShadowState : BaseShadowState
{
    protected SlimeShadowStateMachine StateMachine { get; private set; }

    public SlimeShadowState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
        StateMachine = stateMachine as SlimeShadowStateMachine;
    }
}
