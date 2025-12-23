using System.Collections;
using UnityEngine;

public class SlimeShadowExpandState : SlimeShadowChaseState
{
    private float _maxScale = 2f;
    private bool _isExpanded = false;

    private Coroutine _coroutine;

    public SlimeShadowExpandState(MoveableStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StateMachine.SlimeShadow.MovementSpeedModitier = 2f;
        base.Enter();

        if (_coroutine != null)
        {
            CoroutineRunner.Instance.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        if (!_isExpanded)
        {
            _coroutine = CoroutineRunner.Instance.StartCoroutine(ScaleUp(_maxScale, 0.5f));
            _isExpanded = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // todo: 빛을 맞았을 때 reduce
        if (Input.GetKey(KeyCode.Alpha1))
        {
            Logger.Log("축소 패턴 진입");
            StateMachine.ChangeState(StateMachine.ReduceState);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator ScaleUp(float size, float duration)
    {
        Transform target = StateMachine.SlimeShadow.transform;
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
    }
}
