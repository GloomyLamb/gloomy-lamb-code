using System.Collections;
using UnityEngine;

public class SlimeShadowChaseState : ShadowState
{
    protected SlimeShadow shadow;
    protected SlimeShadowStateMachine stateMachine;

    private float _timer;

    public SlimeShadowChaseState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
        this.shadow = shadow as SlimeShadow;
        this.stateMachine = stateMachine as SlimeShadowStateMachine;
    }

    public override void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > shadow.FastChasePatternTime)
        {
            Logger.Log("정지");
            shadow.PlusChaseCount();
            stateMachine.ChangeState(stateMachine.IdleState);
            _timer = 0f;
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
    }
}
