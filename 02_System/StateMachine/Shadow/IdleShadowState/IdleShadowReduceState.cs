using UnityEngine;

public class IdleShadowReduceState : IdleShadowChaseState
{
    private float _minScale = 1f;

    public IdleShadowReduceState(ShadowStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
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
        if (transform.localScale.x > _minScale)
        {
            transform.localScale -= Vector3.one * Time.deltaTime;
            return;
        }

        transform.localScale = Vector3.one;
        StateMachine.ChangeState(StateMachine.TransformState);
    }
}
