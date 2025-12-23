public class DuskyDieState : BaseDuskyState
{
    public DuskyDieState(StateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }

    public override void Enter()
    {
        player.Animator.SetTrigger(AnimatorParameters.Die);
    }

    public override void Exit()
    {

    }
}