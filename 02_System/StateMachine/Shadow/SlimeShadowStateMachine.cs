using System.Collections;
using UnityEngine;

public class SlimeShadowStateMachine : ShadowStateMachine
{
    public new SlimeShadow Shadow { get; private set; }

    public ShadowState ExpandState { get; private set; }

    public SlimeShadowStateMachine(SlimeShadow shadow) : base(shadow)
    {
        Shadow = shadow;

        ExpandState = new ShadowState(shadow, this);
    }

    public override void Init()
    {
        base.Init();

        ChaseState.Init(MovementType.Run, Shadow.AnimationData.ChaseParameterHash, AnimType.Bool, true);
        ExpandState.Init(MovementType.Stop, Shadow.AnimationData.ChaseParameterHash, AnimType.Bool, true);
    }

    public override void Register()
    {
        base.Register();

        StateFixedUpdateActions[ExpandState] = HandleChaseStateFixedUpdate;

        // Coroutine
        StateCoroutineActions[ChaseState] = HandleChaseStateCoroutine;
        StateCoroutineActions[ExpandState] = HandleExpandStateCoroutine;
    }

    private float _timer;

    protected override void HandleIdleStateUpdate()
    {
        if (Shadow.CurChaseCount == Shadow.TotalChaseCount)
        {
            Logger.Log($"추적 횟수: {Shadow.CurChaseCount} => 확대 패턴 진입");
            ChangeState(ExpandState);
        }

        _timer += Time.deltaTime;
        if (_timer > Shadow.StopPatternTime)
        {
            ChangeState(ChaseState);
            _timer = 0f;
        }
    }

    protected IEnumerator HandleChaseStateCoroutine()
    {
        Shadow.PlusChaseCount();
        SoundManager.Instance.PlaySfxOnce(SfxName.Slime, idx: 2);

        if (Shadow.CurChaseCount <= Shadow.SlowChaseCount)
        {
            //Logger.Log("저속 이동");
            Shadow.SetMovementMultiplier(MovementType.Walk);
            yield return new WaitForSeconds(Shadow.SlowChasePatternTime);
        }
        else
        {
            //Logger.Log("고속 이동");
            yield return new WaitForSeconds(Shadow.FastChasePatternTime);
        }

        ChangeState(IdleState);
    }

    protected IEnumerator HandleExpandStateCoroutine()
    {
        Transform target = Shadow.transform;
        Vector3 startScale = target.localScale;
        Vector3 endScale = startScale * Shadow.MaxScale;
        float elapsed = 0f;

        while (elapsed < Shadow.ScaleUpDuration)
        {
            target.localScale = Vector3.Lerp(startScale, endScale, elapsed / Shadow.ScaleUpDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.localScale = endScale;
        Shadow.CheckExpand();
        ChangeState(ChaseState);
    }
}
