using UnityEngine;

public class DogShadowChaseState : DogShadowGroundState
{
    public DogShadowChaseState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.Shadow.AnimationData.ChaseParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.AnimationData.ChaseParameterHash);
    }

    public override void Update()
    {
        base.Update();

        Transform shadowT = StateMachine.Shadow.transform;
        Transform targetT = StateMachine.Shadow.Target.transform;

        if ((targetT.position - shadowT.position).sqrMagnitude < StateMachine.Shadow.SqrBiteRange)
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
        StateMachine.Shadow.HandleMove();
    }
}
