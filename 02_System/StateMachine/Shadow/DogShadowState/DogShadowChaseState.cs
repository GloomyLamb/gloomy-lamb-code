using UnityEngine;

public class DogShadowChaseState : DogShadowGroundState
{
    public DogShadowChaseState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.DogShadow.AnimationData.ChaseParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.DogShadow.AnimationData.ChaseParameterHash);
    }

    public override void Update()
    {
        base.Update();

        Transform shadowT = StateMachine.DogShadow.transform;
        Transform targetT = StateMachine.DogShadow.Target.transform;

        if ((targetT.position - shadowT.position).sqrMagnitude < StateMachine.DogShadow.SqrBiteRange)
        {
            StateMachine.ChangeState(StateMachine.BiteState);
        }
        else
        {
            // todo - bark 조건 보완
            StateMachine.ChangeState(StateMachine.BarkState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        StateMachine.DogShadow.HandleMove();
    }
}
