public class DogShadowSkillState : DogShadowState
{
    public DogShadowSkillState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.DogShadow.AnimationData.SkillParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.DogShadow.AnimationData.SkillParameterHash);
    }
}
