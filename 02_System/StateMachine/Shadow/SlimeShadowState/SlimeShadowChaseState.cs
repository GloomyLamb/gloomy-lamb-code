using System.Collections;
using UnityEngine;

public class SlimeShadowChaseState : SlimeShadowGroundState
{
    private float _timer;
    private float _patternTime = 1f;

    public SlimeShadowChaseState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _timer = 0f;
        base.Enter();
        StartAnimation(StateMachine.Shadow.AnimationData.ChaseParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.AnimationData.ChaseParameterHash);
    }

    public override void Update()
    {
        base.Update();

        _timer += Time.deltaTime;
        if (_timer > _patternTime)
        {
            Logger.Log("정지");
            StateMachine.PlusChaseCount();
            StateMachine.ChangeState(StateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        StateMachine.Shadow.OnMove?.Invoke();
    }

    protected IEnumerator ScaleUp(float size, float duration)
    {
        Transform target = StateMachine.Shadow.transform;
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
