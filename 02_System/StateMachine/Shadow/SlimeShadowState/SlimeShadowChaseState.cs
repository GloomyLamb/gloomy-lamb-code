public class SlimeShadowChaseState : SlimeShadowGroundState
{
    public SlimeShadowChaseState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.SlimeShadow.AnimationData.ChaseParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.SlimeShadow.AnimationData.ChaseParameterHash);
    }
}
