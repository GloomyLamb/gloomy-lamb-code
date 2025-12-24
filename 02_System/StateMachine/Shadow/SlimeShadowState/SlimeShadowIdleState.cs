using UnityEngine;

public class SlimeShadowIdleState : ShadowIdleState
{
    private SlimeShadow _shadow;
    private SlimeShadowStateMachine _stateMachine;

    private float _timer;
    private float _patternTime = 0.5f;

    public SlimeShadowIdleState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
        _shadow = shadow as SlimeShadow;
        _stateMachine = stateMachine as SlimeShadowStateMachine;
    }

    public override void Enter()
    {
        _timer = 0f;
        base.Enter();
    }

    public override void Update()
    {
        if (_shadow.CurChaseCount == _shadow.ChaseCount + 1)
        {
            Logger.Log("확대 패턴 진입");
            StateMachine.ChangeState(this._stateMachine.ExpandState);
        }

        _timer += Time.deltaTime;
        if (_shadow.Target != null)
        {
            if (_timer > _patternTime && !_shadow.IsFastMode)
            {
                Logger.Log("저속 이동");
                _shadow.IsFastMode = true;
                StateMachine.ChangeState(_stateMachine.WalkState);
            }
            else if (_timer > _patternTime && _shadow.IsFastMode)
            {
                Logger.Log("고속 이동");
                StateMachine.ChangeState(_stateMachine.RunState);
            }
        }
    }
}
