using UnityEngine;

public class SlimeShadowRunState : SlimeShadowChaseState
{
    private float _patternTime;
    private float _timer;

    public SlimeShadowRunState(ShadowStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.SlimeShadow.MovementSpeedModitier = 2f;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        _timer += Time.deltaTime;
        if (_timer > _patternTime)
        {
            _timer = 0f;
            Logger.Log("확대 패턴 진입");
            StateMachine.ChangeState(StateMachine.ExpandState);
        }
    }
}
