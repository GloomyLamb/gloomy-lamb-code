using UnityEngine;

public class SlimeShadowWalkState : SlimeShadowChaseState
{
    private float _timer;

    public SlimeShadowWalkState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _timer = 0f;
        StateMachine.Shadow.SetMovementModifier(MovementType.Default);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > StateMachine.Shadow.SlowChasePatternTime)
        {
            StateMachine.ChangeState(StateMachine.IdleState);
        }
    }
}
