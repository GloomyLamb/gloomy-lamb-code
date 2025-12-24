using System.Collections;
using UnityEngine;

public class ShadowChangeState : CommonShadowState
{
    private Coroutine _coroutine;
    private WaitForSeconds _delay;

    public ShadowChangeState(Shadow shadow, StateMachine stateMachine) : base(shadow, stateMachine)
    {
        _delay = new WaitForSeconds(1f);
    }

    public override void Enter()
    {
        base.Enter();
        shadow.Animator.SetTrigger(shadow.CommonAnimationData.ChangeParameterHash);

        if (_coroutine != null)
        {
            CustomCoroutineRunner.Instance.StopCoroutine(ChangeCoroutine());
            _coroutine = null;
        }
        _coroutine = CustomCoroutineRunner.Instance.StartCoroutine(ChangeCoroutine());
    }

    private IEnumerator ChangeCoroutine()
    {
        yield return _delay;
        shadow.Change();
    }
}
