using UnityEngine;

public class SlimeShadowIdleState : SlimeShadowGroundState
{
    private float _timer;
    private float _patternTime = 0.5f;
    private bool _fastMode;

    public SlimeShadowIdleState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _timer = 0f;
        StateMachine.SlimeShadow.MovementSpeedModitier = 0f;
        base.Enter();
        StartAnimation(StateMachine.SlimeShadow.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.SlimeShadow.AnimationData.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (StateMachine.ChaseCount == 11)
        {
            Logger.Log("확대 패턴 진입");
            StateMachine.ChangeState(StateMachine.ExpandState);
        }

        _timer += Time.deltaTime;
        if (StateMachine.SlimeShadow.Target != null)
        {
            if (_timer > _patternTime && !_fastMode)
            {
                Logger.Log("저속 이동");
                _fastMode = true;
                StateMachine.ChangeState(StateMachine.WalkState);
            }
            else if (_timer > _patternTime && _fastMode)
            {
                Logger.Log("고속 이동");
                StateMachine.ChangeState(StateMachine.RunState);
            }
        }
    }
}
