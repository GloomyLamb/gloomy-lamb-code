using UnityEngine;

public class SlimeShadowWalkState : SlimeShadowChaseState
{
    private float _timer;

    public SlimeShadowWalkState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    public override void Enter()
    {
        _timer = 0f;
        shadow.SetMovementModifier(MovementType.Default);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > shadow.SlowChasePatternTime)
        {
            StateMachine.ChangeState(StateMachine.IdleState);
        }
    }
}
