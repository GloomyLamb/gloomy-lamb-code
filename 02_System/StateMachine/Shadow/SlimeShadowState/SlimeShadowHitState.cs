public class SlimeShadowHitState : SlimeShadowAttackState
{
    public SlimeShadowHitState(MoveableStateMachine stateMachine) : base(stateMachine)
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
        base.Exit();
        StopAnimation(StateMachine.SlimeShadow.AnimationData.HitParameterHash);
    }
}
