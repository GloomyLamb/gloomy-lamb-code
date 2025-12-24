using System.Collections;
using UnityEngine;

public class ShadowTransformState : CommonShadowState
{
    private Coroutine _coroutine;
    private WaitForSeconds _delay;

    public ShadowTransformState(Shadow shadow, StateMachine stateMachine) : base(shadow, stateMachine)
    {
        _delay = new WaitForSeconds(1f);
    }

    public override void Enter()
    {
        base.Enter();
        shadow.Animator.SetTrigger(shadow.CommonAnimationData.TransformParameterHash);

        if (_coroutine != null)
        {
            CustomCoroutineRunner.Instance.StopCoroutine(TransformCoroutine());
            _coroutine = null;
        }
        _coroutine = CustomCoroutineRunner.Instance.StartCoroutine(TransformCoroutine());
    }

    private IEnumerator TransformCoroutine()
    {
        yield return _delay;
        shadow.Transform();
    }
}
