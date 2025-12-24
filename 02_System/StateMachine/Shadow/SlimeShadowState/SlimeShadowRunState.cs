public class SlimeShadowRunState : SlimeShadowChaseState
{
    public SlimeShadowRunState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
        movementType = MovementType.Run;
    }
}
