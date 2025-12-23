public class DogShadowBarkState : DogShadowSkillState
{
    public DogShadowBarkState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.Shadow.AnimationData.BarkParameterHash);
        // todo: 짖기 스킬 연결
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.AnimationData.BarkParameterHash);
    }
}
