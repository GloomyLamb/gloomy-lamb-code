public class DogShadowBiteState : DogShadowSkillState
{
    public DogShadowBiteState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.DogShadow.BiteCount++;
        base.Enter();
        StartAnimation(StateMachine.DogShadow.AnimationData.BiteParameterHash);
        // todo: 물기 스킬 연결
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.DogShadow.AnimationData.BiteParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (StateMachine.DogShadow.BiteCount > 3)
        {
            Logger.Log("짖기 3회 넘음");
            StateMachine.ChangeState(StateMachine.BarkState);
        }
    }
}
