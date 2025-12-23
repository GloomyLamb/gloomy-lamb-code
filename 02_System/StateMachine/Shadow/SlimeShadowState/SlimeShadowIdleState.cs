public class SlimeShadowIdleState : SlimeShadowGroundState
{
    public SlimeShadowIdleState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.SlimeShadow.MovementSpeedModitier = 0f;
        base.Enter();
        StartAnimation(StateMachine.SlimeShadow.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.SlimeShadow.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
        if (StateMachine.SlimeShadow.Target != null)
        {
            StateMachine.ChangeState(StateMachine.WalkState);
        }
    }
}
