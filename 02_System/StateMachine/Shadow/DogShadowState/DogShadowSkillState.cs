public class DogShadowSkillState : DogShadowState
{
    public DogShadowSkillState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.Shadow.SkillAnimationData.SkillParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.SkillAnimationData.SkillParameterHash);
    }
}
