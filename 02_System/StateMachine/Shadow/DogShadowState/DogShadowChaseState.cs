public class DogShadowChaseState : DogShadowGroundState
{
    public DogShadowChaseState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.DogShadow.AnimationData.ChaseParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.DogShadow.AnimationData.ChaseParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        StateMachine.DogShadow.HandleMove();
    }
}
