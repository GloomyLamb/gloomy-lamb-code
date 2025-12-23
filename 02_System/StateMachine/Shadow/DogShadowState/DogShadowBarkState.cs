public class DogShadowBarkState : DogShadowSkillState
{
    public DogShadowBarkState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.DogShadow.AnimationData.BarkParameterHash);
        // todo: 짖기 스킬 연결
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.DogShadow.AnimationData.BarkParameterHash);
    }
}
