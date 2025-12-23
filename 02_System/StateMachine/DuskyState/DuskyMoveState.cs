public class DuskyMoveState : BaseDuskyState, IMovableState
{
    public DuskyMoveState(StateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }

    public override void Enter()
    {
        StartAnimation(AnimatorParameters.IsMove);
        player.Animator.SetFloat(AnimatorParameters.MoveSpeed, 0);
    }

    public override void Exit()
    {
        StopAnimation(AnimatorParameters.IsMove);
    }
}