using UnityEngine;

public class IdleShadowExpandState : IdleShadowChaseState
{
    private float _maxScale;

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
    }
}
