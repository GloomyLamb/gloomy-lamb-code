using UnityEngine;

public class SlimeShadowChaseState : SlimeShadowGroundState
{
    private float _timer;
    private float _patternTime = 1f;

    public SlimeShadowChaseState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _timer = 0f;
        base.Enter();
        StartAnimation(StateMachine.SlimeShadow.AnimationData.ChaseParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.SlimeShadow.AnimationData.ChaseParameterHash);
    }

    public override void Update()
    {
        base.Update();

        _timer += Time.deltaTime;
        if (_timer > _patternTime)
        {
            Logger.Log("정지");
            StateMachine.PlusChaseCount();
            StateMachine.ChangeState(StateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        StateMachine.SlimeShadow.HandleMove();
    }
}
