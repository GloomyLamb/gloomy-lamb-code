using UnityEngine;

public class SlimeShadowChaseState : ShadowState
{
    protected SlimeShadow shadow;
    protected SlimeShadowStateMachine stateMachine;

    private float _timer;

    public SlimeShadowChaseState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
        this.shadow = shadow as SlimeShadow;
        this.stateMachine = stateMachine as SlimeShadowStateMachine;
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > shadow.FastChasePatternTime)
        {
            Logger.Log("정지");
            shadow.PlusChaseCount();
            stateMachine.ChangeState(stateMachine.IdleState);
            _timer = 0f;
        }
    }
}
