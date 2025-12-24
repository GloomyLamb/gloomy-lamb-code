using UnityEngine;

public class DogShadowBarkState : DogShadowSkillState
{
    private float _timer;
    private float _patternTime = 1f;

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
    }
}
