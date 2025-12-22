using UnityEngine;

public class IdleShadowWalkState : IdleShadowChaseState
{
    private float _patternTime;
    private float _timer;

    public IdleShadowWalkState(ShadowStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.IdleShadow.MovementSpeedModitier = 1.3f;
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
            StateMachine.ChangeState(StateMachine.RunState);
        }
    }
}
