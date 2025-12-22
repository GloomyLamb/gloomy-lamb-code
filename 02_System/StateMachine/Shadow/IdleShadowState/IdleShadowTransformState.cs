public class IdleShadowTransformState : IdleShadowGroundState
{
    public IdleShadowTransformState(ShadowStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.IdleShadow.MovementSpeedModitier = 0f;
        base.Enter();
    }
}
