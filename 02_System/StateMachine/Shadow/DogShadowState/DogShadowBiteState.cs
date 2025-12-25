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
        Logger.Log("물기 상태");
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
    }

    /// <summary>
    /// 물기 패턴 전 플레이어 방향으로 회전
    /// </summary>
    /// <returns></returns>
    private IEnumerator BiteCoroutine()
    {
        DogShadow shadow = StateMachine.Shadow;
        Vector3 start = shadow.transform.position;
        Vector3 end = shadow.Target.position;       // 플레이어 위치 고정

        float elapsed = 0f;

        while (elapsed < shadow.BiteDuration)
        {
            shadow.transform.position = Vector3.Lerp(start, end, elapsed / shadow.BiteDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        shadow.transform.position = end;

        StartAnimation(shadow.SkillAnimationData.BiteParameterHash);

        shadow.Bite();
    }
}
