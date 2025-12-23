using UnityEngine;

public class IdleShadowRunState : IdleShadowChaseState
{
    private float _patternTime;
    private float _timer;

    public IdleShadowRunState(ShadowStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.IdleShadow.MovementSpeedModitier = 2f;
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
