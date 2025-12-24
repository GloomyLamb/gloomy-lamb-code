using UnityEngine;

public class SlimeShadowReduceState : SlimeShadowChaseState
{
    private Coroutine _coroutine;

    public SlimeShadowReduceState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        CustomCoroutineRunner.Instance.StopCoroutine(_coroutine);
        _coroutine = null;
    }

    public override void Update()
    {
        base.Update();

        if (!shadow.IsHitting)
        {
            return;
        }

        if (_coroutine != null)
        {
            CustomCoroutineRunner.Instance.StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _coroutine = CustomCoroutineRunner
            .Instance
            .StartCoroutine(ScaleUp(shadow.MinScale, 0.5f));
    }
}
