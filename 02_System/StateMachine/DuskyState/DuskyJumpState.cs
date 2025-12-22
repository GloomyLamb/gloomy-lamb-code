public class DuskyJumpState : BaseDuskyState
{
    public DuskyJumpState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.animator.SetTrigger(AnimatorParameters.Jump);
        stateMachine.animator.SetBool(AnimatorParameters.IsJumping, true);
    }
    
    public override void Update()
    {
        base.Update();
    }
    
    public override void Exit()
    {
        stateMachine.animator.SetBool(AnimatorParameters.IsJumping, false);
    }
}