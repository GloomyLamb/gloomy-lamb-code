public class DuskyJumpState : BaseDuskyState, IMovableState
{
    public DuskyJumpState(StateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }

    public override void Enter()
    {
        player.Animator.SetTrigger(AnimatorParameters.Jump);
        player.Animator.SetBool(AnimatorParameters.IsFalling, true);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        player.Animator.SetBool(AnimatorParameters.IsFalling, false);
    }
}