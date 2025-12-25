using System.Collections;
using UnityEngine;

public class DogShadowBiteState : DogShadowSkillState
{
    public DogShadowBiteState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }

    public override void Enter()
    {
        Logger.Log("물기");
        StartAnimation(StateMachine.Shadow.SkillAnimationData.SkillParameterHash);

        if (useCoroutine)
        {
            StartCoroutine();
        }
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(StateMachine.Shadow.SkillAnimationData.SkillParameterHash);
    }

    /// <summary>
    /// 물기 패턴 전 플레이어 방향으로 회전
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator StateCoroutine()
    {
        DogShadow shadow = StateMachine.Shadow;
        Transform target = shadow.Controller.transform;
        Quaternion startRot = target.rotation;

        Vector3 dir = shadow.Target.position - target.position;
        dir.y = 0f; // 수평 회전만 필요하다면

        yield return null;
        //Quaternion targetRot = Quaternion.LookRotation(dir);

        //float elapsed = 0f;

        //while (elapsed < shadow.BiteDuration)
        //{
        //    target.rotation = Quaternion.Slerp(
        //        startRot,
        //        targetRot,
        //        elapsed / shadow.BiteDuration
        //    );

        //    elapsed += Time.deltaTime;
        //    yield return null;
        //}

        //target.rotation = targetRot;

        StartAnimation(shadow.SkillAnimationData.BiteParameterHash);
        shadow.Bite();
    }
}
