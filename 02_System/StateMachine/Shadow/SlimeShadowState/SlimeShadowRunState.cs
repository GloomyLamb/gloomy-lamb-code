using UnityEngine;

public class SlimeShadowRunState : SlimeShadowChaseState
{
    private float _patternTime;
    private float _timer;

    public SlimeShadowRunState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _timer = 0f;
        StateMachine.SlimeShadow.MovementSpeedModitier = 2f;
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
            Logger.Log("확대 패턴 진입");
            StateMachine.ChangeState(StateMachine.ExpandState);
        }
    }
}
