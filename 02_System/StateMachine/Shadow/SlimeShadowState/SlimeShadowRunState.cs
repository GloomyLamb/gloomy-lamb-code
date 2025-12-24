public class SlimeShadowRunState : SlimeShadowChaseState
{
    public SlimeShadowRunState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.Shadow.SetMovementModifier(MovementType.Run);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
