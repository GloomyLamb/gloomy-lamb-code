public class IdleShadowWalkState : IdleShadowChaseState
{
    public IdleShadowWalkState(ShadowStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.IdleShadow.MovementSpeedModitier = 1.3f;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
