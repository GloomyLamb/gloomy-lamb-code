public class IdleShadowHitState : IdleShadowAttackState
{
    public IdleShadowHitState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.IdleShadow.MovementSpeedModitier = 0f;
        base.Enter();
        StartAnimation(StateMachine.IdleShadow.AnimationData.HitParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.IdleShadow.AnimationData.HitParameterHash);
    }
}
