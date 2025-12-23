using UnityEngine;

public class SlimeShadowHitState : SlimeShadowAttackState
{
    private float _timer;
    private float _animationTime = 1f;

    public SlimeShadowHitState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _timer = 0f;
        StateMachine.Shadow.MovementSpeedModitier = 0f;
        base.Enter();
        StartAnimation(StateMachine.Shadow.AnimationData.HitParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.AnimationData.HitParameterHash);
    }

    public override void Update()
    {
        base.Update();

        _timer += Time.deltaTime;
        if (_timer > _animationTime)
        {
            StateMachine.ChangeState(StateMachine.IdleState);
        }
    }
}
