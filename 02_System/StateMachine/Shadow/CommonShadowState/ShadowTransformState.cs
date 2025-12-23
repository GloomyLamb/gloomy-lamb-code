public class ShadowTransformState : CommonShadowState
{
    public ShadowTransformState(Shadow shadow, StateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        shadow.Animator.SetTrigger(shadow.CommonAnimationData.TransformParameterHash);
    }
}
