using UnityEngine;

public class SlimeShadowReduceState : SlimeShadowChaseState
{
    private float _minScale = 1f;

    private Coroutine _coroutine;

    public SlimeShadowReduceState(StateMachine stateMachine) : base(stateMachine)
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

        if (!StateMachine.Shadow.IsHitting)
        {
            return;
        }

        if (_coroutine != null)
        {
            CustomCoroutineRunner.Instance.StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _coroutine = CustomCoroutineRunner.Instance.StartCoroutine(ScaleUp(_minScale, 0.5f));
    }
}
