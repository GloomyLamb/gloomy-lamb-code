public class SlimeShadowBoundState : SlimeShadowAttackState
{
    public SlimeShadowBoundState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.SlimeShadow.MovementSpeedModitier = 0f;
        base.Enter();
        StartAnimation(StateMachine.SlimeShadow.AnimationData.HitParameterHash);
    }

    public override void Exit()
    {
        StateMachine.SlimeShadow.MovementSpeedModitier = 0f;
        base.Exit();
        StopAnimation(StateMachine.SlimeShadow.AnimationData.HitParameterHash);
    }
}
