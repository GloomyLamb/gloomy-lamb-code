public class ShadowBoundState : CommonShadowState
{
    public ShadowBoundState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    public override void Enter()
    {
        shadow.SetMovementModifier(MovementType.Stop);
        base.Enter();
        shadow.Animator.SetTrigger(shadow.AnimationData.HitParameterHash);
    }
}
