public class IdleShadowBoundState : IdleShadowAttackState
{
    public IdleShadowBoundState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.IdleShadow.MovementSpeedModitier = 0f;
        base.Enter();
        StartAnimation(StateMachine.IdleShadow.AnimationData.BoundParameterHash);
    }

    public override void Exit()
    {
        StateMachine.IdleShadow.MovementSpeedModitier = 0f;
        base.Exit();
        StopAnimation(StateMachine.IdleShadow.AnimationData.BoundParameterHash);
    }
}
