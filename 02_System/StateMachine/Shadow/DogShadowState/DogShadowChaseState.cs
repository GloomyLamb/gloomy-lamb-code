using UnityEngine;

public class DogShadowChaseState : ShadowChaseState
{
    private DogShadowStateMachine _stateMachine;

    public DogShadowChaseState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
        _stateMachine = stateMachine as DogShadowStateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        Transform shadowT = shadow.transform;
        Transform targetT = shadow.Target.transform;

        if ((targetT.position - shadowT.position).sqrMagnitude < _stateMachine.Shadow.SqrBiteRange)
        {
            StateMachine.ChangeState(_stateMachine.BiteState);
        }
        else
        {
            // todo - bark 조건 보완
            StateMachine.ChangeState(_stateMachine.BarkState);
        }
    }
}
