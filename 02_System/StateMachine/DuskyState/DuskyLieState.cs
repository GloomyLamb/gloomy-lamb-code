public class DuskyLieState : BaseDuskyState
{
    public DuskyLieState(StateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }

    public override void Enter()
    {
        player.Animator.SetTrigger(AnimatorParameters.Die);
        player.Animator.SetBool(AnimatorParameters.IsDead, true);
    }

    public override void Update()
    {
        if (player.NowCondition.HasFlag(CharacterCondition.Stun) == false)
        {
            stateMachine.ChangeState(stateMachine.IdleState);           
        }
    }

    public override void Exit()
    {
        player.Animator.SetBool(AnimatorParameters.IsDead, false);
    }
}