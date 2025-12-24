public class DogShadowIdleState : DogShadowGroundState
{
    public DogShadowIdleState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.Shadow.SetMovementModifier(MovementType.Stop);
        base.Enter();
        StartAnimation(StateMachine.Shadow.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (StateMachine.Shadow.Target != null)
        {
            StateMachine.ChangeState(StateMachine.ChaseState);
        }
    }
}
