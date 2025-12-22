public class IdleShadowRunState : IdleShadowChaseState
{
    public IdleShadowRunState(ShadowStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.IdleShadow.MovementSpeedModitier = 2f;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
