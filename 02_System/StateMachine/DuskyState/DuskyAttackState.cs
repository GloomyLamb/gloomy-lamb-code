public class DuskyAttackState : BaseDuskyState
{
    public DuskyAttackState(MoveableStateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
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