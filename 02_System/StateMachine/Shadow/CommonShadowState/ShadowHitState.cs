using UnityEngine;

public class ShadowHitState : ShadowState
{
    private float _timer;

    public ShadowHitState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    protected override void ResetParameter()
    {
        _timer = 0f;
    }

    public override void Update()
    {
        base.Update();

        _timer += Time.deltaTime;
        if (_timer > shadow.HitDuration)
        {
            StateMachine.ChangeState(StateMachine.IdleState);
        }
    }
}
