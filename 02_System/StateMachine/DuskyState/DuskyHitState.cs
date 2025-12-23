public class DuskyHitState : BaseDuskyState
{
    public DuskyHitState(MoveableStateMachine stateMachine, DuskyPlayer player) : base(stateMachine, player)
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