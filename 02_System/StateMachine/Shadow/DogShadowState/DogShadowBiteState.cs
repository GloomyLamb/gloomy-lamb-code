using UnityEngine;

public class DogShadowBiteState : DogShadowSkillState
{
    private float _timer;
    private float _patternTime = 1f;

    public DogShadowBiteState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StateMachine.Shadow.BiteCount++;
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

        _timer += Time.deltaTime;
        if (_timer > _patternTime)
        {
            _timer = 0f;
            StateMachine.Shadow.DonePattern = true;
            StateMachine.ChangeState(StateMachine.IdleState);
        }

        if (StateMachine.Shadow.BiteCount > 3)
        {
            Logger.Log("짖기 3회 넘음");
            StateMachine.ChangeState(StateMachine.IdleState);
        }
    }
}
