public class DuskyDieState : BaseDuskyState
{
    public DuskyDieState(MoveableStateMachine stateMachine) : base(stateMachine)
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