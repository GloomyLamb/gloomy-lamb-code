using UnityEngine;

public class DogShadowChaseState : DogShadowGroundState
{
    private Transform _shadowT;
    private Transform _targetT;

    public DogShadowChaseState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (_shadowT == null)
        {
            _shadowT = StateMachine.DogShadow.transform;
        }

        if (_targetT == null)
        {
            _targetT = StateMachine.DogShadow.Target;
        }

        StartAnimation(StateMachine.DogShadow.AnimationData.ChaseParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.DogShadow.AnimationData.ChaseParameterHash);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Move();
    }

    private void Move()
    {
        Vector3 dir = (_shadowT.position - _targetT.position).normalized;
        dir.y = 0f;
        _shadowT.position += dir * StateMachine.DogShadow.MovementSpeed;
    }
}
