using UnityEngine;

public class SlimeShadowReduceState : SlimeShadowChaseState
{
    private float _minScale = 1f;

    public SlimeShadowReduceState(ShadowStateMachine stateMachine) : base(stateMachine)
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

        Transform transform = StateMachine.SlimeShadow.transform;

        // todo: 광선에 맞고 있는지 계속 확인
        if (transform.localScale.x > _minScale)
        {
            transform.localScale -= Vector3.one * Time.deltaTime;
            return;
        }

        transform.localScale = Vector3.one;
        StateMachine.ChangeState(StateMachine.TransformState);
    }
}
