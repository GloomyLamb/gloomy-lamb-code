public class DuskyDashState:BaseDuskyState, IMovableState
{
    public DuskyDashState(StateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }

    public override void Enter()
    {
        StartAnimation(AnimatorParameters.IsMove);
        player.Animator.SetFloat(AnimatorParameters.MoveSpeed, 1);
        
    }

    public override void Exit()
    {
        StopAnimation(AnimatorParameters.IsMove);
    }
}