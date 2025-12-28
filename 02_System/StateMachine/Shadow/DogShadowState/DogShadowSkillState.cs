public class DogShadowSkillState : DogShadowState
{
    public DogShadowSkillState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        shadow.Animator.SetTrigger(shadow.SkillAnimationData.SkillParameterHash);
    }
}
