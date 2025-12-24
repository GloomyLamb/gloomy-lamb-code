using System.Collections;
using UnityEngine;

public class SlimeShadowChaseState : ShadowChaseState
{
    protected SlimeShadow shadow;
    protected SlimeShadowStateMachine stateMachine;

    private float _timer;

    public SlimeShadowChaseState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
        this.shadow = shadow as SlimeShadow;
        this.stateMachine = stateMachine as SlimeShadowStateMachine;
    }

    public override void Enter()
    {
        _timer = 0f;
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        _timer += Time.deltaTime;
        if (_timer > shadow.FastChasePatternTime)
        {
            Logger.Log("정지");
            shadow.PlusChaseCount();
            stateMachine.ChangeState(stateMachine.IdleState);
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
