using UnityEngine;

public class SlimeShadowWalkState : SlimeShadowChaseState
{
    private float _patternTime;
    private float _timer;

    public SlimeShadowWalkState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _timer = 0f;
        StateMachine.Shadow.MovementSpeedModitier = 1.3f;
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
            StateMachine.ChangeState(StateMachine.RunState);
        }
    }
}
