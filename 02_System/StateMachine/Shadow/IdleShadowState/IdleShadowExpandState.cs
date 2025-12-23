using UnityEngine;

public class IdleShadowExpandState : IdleShadowChaseState
{
    private float _maxScale = 2f;

    public IdleShadowExpandState(ShadowStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.IdleShadow.MovementSpeedModitier = 2f;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        Transform transform = StateMachine.IdleShadow.transform;
        if (transform.localScale.x < _maxScale)
        {
            transform.localScale += Vector3.one * Time.deltaTime;
        }

        // todo: 빛을 맞았을 때 reduce
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Logger.Log("축소 패턴 진입");
            StateMachine.ChangeState(StateMachine.ReduceState);
        }
    }
}
