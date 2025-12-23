public class DuskyJumpState : BaseDuskyState, IMovableState
{
    public DuskyJumpState(MoveableStateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
    {
    }

    public override void Enter()
    {
        stateMachine.animator.SetTrigger(AnimatorParameters.Jump);
        stateMachine.animator.SetBool(AnimatorParameters.IsFalling, true);
    }
    
    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        stateMachine.animator.SetBool(AnimatorParameters.IsFalling, false);
    }
}