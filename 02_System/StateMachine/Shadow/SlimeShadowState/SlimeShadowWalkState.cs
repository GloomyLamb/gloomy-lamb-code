using UnityEngine;

public class SlimeShadowWalkState : SlimeShadowChaseState
{
    private float _timer;

    public SlimeShadowWalkState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    protected override void ResetParameter()
    {
        base.ResetParameter();
        _timer = 0f;
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
