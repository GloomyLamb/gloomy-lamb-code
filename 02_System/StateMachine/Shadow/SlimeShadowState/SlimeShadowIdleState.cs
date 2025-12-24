using UnityEngine;

public class SlimeShadowIdleState : SlimeShadowGroundState
{
    private float _timer;
    private float _patternTime = 0.5f;

    public SlimeShadowIdleState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _timer = 0f;
        StateMachine.Shadow.SetMovementModifier(MovementType.Stop);
        base.Enter();
        StartAnimation(StateMachine.Shadow.AnimationData.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.AnimationData.IdleParameterHash);
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
        if (StateMachine.Shadow.Target != null)
        {
            if (_timer > _patternTime && !StateMachine.Shadow.IsFastMode)
            {
                Logger.Log("저속 이동");
                StateMachine.Shadow.IsFastMode = true;
                StateMachine.ChangeState(StateMachine.WalkState);
            }
            else if (_timer > _patternTime && StateMachine.Shadow.IsFastMode)
            {
                Logger.Log("고속 이동");
                StateMachine.ChangeState(StateMachine.RunState);
            }
        }
    }
}
