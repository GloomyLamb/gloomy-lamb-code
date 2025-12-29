using System.Collections;
using UnityEngine;

public class DogShadowStateMachine : ShadowStateMachine
{
    public new DogShadow Shadow { get; private set; }

    // Skill
    public ShadowState BiteState { get; private set; }
    public ShadowState BackwardState { get; private set; }
    public ShadowState BarkState { get; private set; }

    public DogShadowStateMachine(DogShadow shadow) : base(shadow)
    {
        Shadow = shadow;

        BiteState = new ShadowSkillState(Shadow, this);
        BackwardState = new ShadowState(Shadow, this);
        BarkState = new ShadowSkillState(Shadow, this);
    }

    public override void Init()
    {
        base.Init();
        BiteState.Init(MovementType.Stop, Shadow.SkillAnimationData.BiteParameterHash, AnimType.Bool, true);
        BackwardState.Init(MovementType.Walk, Shadow.SkillAnimationData.BackwardParameterHash, AnimType.Bool, true);
        BarkState.Init(MovementType.Stop, Shadow.SkillAnimationData.BarkParameterHash, AnimType.Bool, true);
    }

    public override void Register()
    {
        base.Register();

        StateCoroutineActions[IdleState] = HandleIdleStateCoroutine;
        StateCoroutineActions[BiteState] = HandleBiteStateCoroutine;
        StateCoroutineActions[BackwardState] = HandleBackwardStateCoroutine;
        StateCoroutineActions[BarkState] = HandleBarkStateCoroutine;
    }

    protected override void HandleChaseStateUpdate()
    {
        base.HandleChaseStateUpdate();

        Transform shadowT = Shadow.transform;
        Transform targetT = Shadow.Target.transform;

        if ((targetT.position - shadowT.position).sqrMagnitude < Shadow.SqrBiteRange &&
            Shadow.BiteCount < Shadow.BiteSuccessThreshold)
        {
            ChangeState(BiteState);
        }
        else
        {
            ChangeState(BarkState);
        }
    }

    #region 상태 Coroutine 내부 로직
    private IEnumerator HandleIdleStateCoroutine()
    {
        Shadow.SetCollisionDamage(10f);
        yield return null;
    }

    private IEnumerator HandleBarkStateCoroutine()
    {
        Shadow.DonePattern = true;
        Shadow.SetCollisionDamage(30f);
        WaitForSeconds spawnTimeSec = new WaitForSeconds(Shadow.BarkPrefabSpawnTime);
        //shadow.HowlEffectPrefab.SetActive(true);

        // 멈출 때까지 딜레이 주기
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < Shadow.BarkCount; ++i)
        {
            SoundManager.Instance.PlaySfxOnce(SfxName.Bark, idx: 2);
            Shadow.SpawnHowlWind();
            yield return spawnTimeSec;
        }

        //shadow.HowlEffectPrefab.SetActive(false);
        Shadow.SetCollisionDamage(10f);
        ChangeState(IdleState);
    }

    private IEnumerator HandleBiteStateCoroutine()
    {
        Transform target = Shadow.Controller.transform;
        Quaternion startRot = target.rotation;

        Vector3 dir = Shadow.Target.position - target.position;
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

        Shadow.Animator.SetBool(Shadow.SkillAnimationData.BiteParameterHash, true);

        if (Shadow.TryBite())
        {
            Shadow.DonePattern = true;
            yield return new WaitForSeconds(Shadow.BiteDuration);
            ChangeState(BackwardState);
        }
        else
        {
            Logger.Log("물기 공격 실패 -> 추적 모드");
            ChangeState(BoundState);
        }
    }

    private IEnumerator HandleBackwardStateCoroutine()
    {
        Shadow.Controller.SetActiveAgentRotation(false);
        Logger.Log("회전 false 상태");
        Shadow.SetMovementMultiplier(MovementType.Walk);
        Shadow.Backward();
        while (Shadow.Controller.Agent.pathPending
            || Shadow.Controller.Agent.remainingDistance > Shadow.Controller.Agent.stoppingDistance)
        {
            Logger.Log("while문 내부");
            yield return null;
        }
        Logger.Log("뒷걸음질 시간동안 대기 완료");
        Shadow.Controller.SetActiveAgentRotation(true);
        Logger.Log("회전 true 상태");
        Logger.Log("패턴 완료");

        ChangeState(IdleState);
    }
    #endregion
}
