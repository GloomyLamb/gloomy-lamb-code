public class IdleShadowGroundState : IdleShadowState
{
    public IdleShadowGroundState(ShadowStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.IdleShadow.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.IdleShadow.AnimationData.GroundParameterHash);
    }
}
