public class ShadowBoundState : CommonShadowState
{
    public ShadowBoundState(Shadow shadow, StateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    public override void Enter()
    {
        shadow.MovementSpeedModitier = 0f;
        base.Enter();
        shadow.Animator.SetTrigger(shadow.CommonAnimationData.HitParameterHash);
    }
}
