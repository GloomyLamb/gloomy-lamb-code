public class DuskyMoveState : BaseDuskyState, IMovableState
{
    public DuskyMoveState(MoveableStateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }

    public override void Enter()
    {
        StartAnimation(AnimatorParameters.IsMove);
    }

    public override void Exit()
    {
        StopAnimation(AnimatorParameters.IsMove);
    }
}