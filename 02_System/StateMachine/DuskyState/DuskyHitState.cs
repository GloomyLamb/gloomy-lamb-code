public class DuskyHitState : BaseDuskyState
{
    public DuskyHitState(StateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }

    public override void Enter()
    {
        player.Animator.SetBool(AnimatorParameters.IsMove, true);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        player.Animator.SetBool(AnimatorParameters.IsMove, false);
    }
}