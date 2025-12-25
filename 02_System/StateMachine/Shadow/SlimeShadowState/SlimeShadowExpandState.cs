using System.Collections;
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
                .StartCoroutine(ScaleTo(shadow.MaxScale, 3f));
            _isExpanded = true;
        }
    }

    protected IEnumerator ScaleTo(float size, float duration)
    {
        Transform target = shadow.transform;
        Vector3 startScale = target.localScale;
        Vector3 endScale = startScale * size;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            target.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.localScale = endScale;
        shadow.CheckExpand();
    }
}
