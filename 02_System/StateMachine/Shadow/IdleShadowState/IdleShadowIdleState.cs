public class IdleShadowIdleState : IdleShadowGroundState
{
    public IdleShadowIdleState(ShadowStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.IdleShadow.MovementSpeedModitier = 0f;
        base.Enter();
        StartAnimation(StateMachine.IdleShadow.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.IdleShadow.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (StateMachine.IdleShadow.Target != null)
        {
            StateMachine.ChangeState(StateMachine.WalkState);
        }
    }
}
