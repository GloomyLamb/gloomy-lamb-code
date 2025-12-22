public class DuskyAttackState : BaseDuskyState
{
    public DuskyAttackState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.animator.SetTrigger(AnimatorParameters.Attack);
    }
    
    public override void Exit()
    {
        base.Exit();
    }
}