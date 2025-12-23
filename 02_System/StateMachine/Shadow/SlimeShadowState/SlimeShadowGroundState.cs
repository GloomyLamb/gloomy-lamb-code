using UnityEngine;

public class SlimeShadowGroundState : SlimeShadowState
{
    public SlimeShadowGroundState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(StateMachine.Shadow.AnimationData.GroundParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.AnimationData.GroundParameterHash);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKey(KeyCode.Alpha2))
        {
            StateMachine.ChangeState(StateMachine.HitState);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            StateMachine.ChangeState(StateMachine.BoundState);
        }
    }
}
