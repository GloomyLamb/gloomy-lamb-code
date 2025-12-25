using System.Collections;
using UnityEngine;

public class DogShadowBiteState : DogShadowSkillState
{
    Coroutine _coroutine;

    public DogShadowBiteState(StateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (StateMachine.Shadow.Target == null)
        {
            Logger.Log("타겟 없음");
        }

        if (_coroutine != null)
        {
            CustomCoroutineRunner.Instance.StopCoroutine(_coroutine);
            _coroutine = null;
        }
        _coroutine = CustomCoroutineRunner.Instance.StartCoroutine(BiteCoroutine());
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.SkillAnimationData.BiteParameterHash);
        Logger.Log("물기");
    }

    private IEnumerator BiteCoroutine()
    {
        DogShadow shadow = StateMachine.Shadow;
        Vector3 start = shadow.transform.position;
        Vector3 end = shadow.Target.position;       // 플레이어 위치 고정

        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            shadow.transform.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        shadow.transform.position = end;

        StartAnimation(shadow.SkillAnimationData.BiteParameterHash);

        shadow.Bite();
    }
}
