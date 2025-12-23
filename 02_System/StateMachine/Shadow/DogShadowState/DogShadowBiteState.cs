public class DogShadowBiteState : DogShadowSkillState
{
    public DogShadowBiteState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.Shadow.BiteCount++;
        base.Enter();
        StartAnimation(StateMachine.Shadow.AnimationData.BiteParameterHash);
        // todo: 물기 스킬 연결
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.AnimationData.BiteParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (StateMachine.Shadow.BiteCount > 3)
        {
            Logger.Log("짖기 3회 넘음");
            StateMachine.ChangeState(StateMachine.BarkState);
        }
    }
}
