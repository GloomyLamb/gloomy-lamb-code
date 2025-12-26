public class DogShadowSkillState : DogShadowState
{
    public DogShadowSkillState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    public override void Enter()
    {
        StartAnimation(StateMachine.Shadow.SkillAnimationData.SkillParameterHash);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.SkillAnimationData.SkillParameterHash);
    }
}
