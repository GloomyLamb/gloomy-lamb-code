using UnityEngine;

public class SlimeShadowExpandState : SlimeShadowChaseState
{
    private bool _isExpanded = false;

    private Coroutine _coroutine;

    public SlimeShadowExpandState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    public override void Enter()
    {
        shadow.SetMovementModifier(MovementType.Run);
        shadow.CheckExpand();
        base.Enter();

        if (_coroutine != null)
        {
            CustomCoroutineRunner.Instance.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        if (!_isExpanded)
        {
            _coroutine = CustomCoroutineRunner
                .Instance
                .StartCoroutine(ScaleTo(shadow.MaxScale, 0.5f));
            _isExpanded = true;
        }
    }
}
