public class IdleShadowChaseState : IdleShadowGroundState
{
    public IdleShadowChaseState(ShadowStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.IdleShadow.AnimationData.ChaseParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.IdleShadow.AnimationData.ChaseParameterHash);
    }
}
