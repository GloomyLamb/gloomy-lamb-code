public class DogShadowIdleState : DogShadowGroundState
{
    public DogShadowIdleState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.DogShadow.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.DogShadow.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (StateMachine.DogShadow.Target != null)
        {
            StateMachine.ChangeState(StateMachine.ChaseState);
        }
    }
}
