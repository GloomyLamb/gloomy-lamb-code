public class DuskyHitState : BaseDuskyState,IMovableState
{
    public DuskyHitState(StateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }

    public override void Enter()
    {
        player.Animator.SetTrigger(AnimatorParameters.Hit);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
    }
}