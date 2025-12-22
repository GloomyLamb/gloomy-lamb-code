public class DuskyHitState : BaseDuskyState
{
    public DuskyHitState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.animator.SetBool(AnimatorParameters.IsMove, true);
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        stateMachine.animator.SetBool(AnimatorParameters.IsMove, false);
    }
}