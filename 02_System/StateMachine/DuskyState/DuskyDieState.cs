public class DuskyDieState : BaseDuskyState
{
    public DuskyDieState(MoveableStateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }

    public override void Enter()
    {
        stateMachine.animator.SetTrigger(AnimatorParameters.Die);
    }

    public override void Exit()
    {
        
    }
}