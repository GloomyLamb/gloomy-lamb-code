using System.Collections;
using UnityEngine;

/// <summary>
/// 그림자 상태 머신 - 슬라임
/// </summary>
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
        StateCoroutineActions[ExpandState] = HandleExpandStateCoroutine;
    }

    private float _idleTimer;
    private float _chaseTimer;

    protected override void HandleIdleStateUpdate()
    {
        Shadow.SetCollisionDamage(Shadow.SlowCollisionDamage);
        if (Shadow.CurChaseCount == Shadow.TotalChaseCount)
        {
            Logger.Log($"추적 횟수: {Shadow.CurChaseCount} => 확대 패턴 진입");
            ChangeState(ExpandState);
        }

        _idleTimer += Time.deltaTime;
        if (hasBeenBound || _idleTimer > Shadow.StopPatternTime)
        {
            hasBeenBound = false;
            ChangeState(ChaseState);
            _idleTimer = 0f;
        }
    }

    protected override void HandleChaseStateUpdate()
    {
        base.HandleChaseStateUpdate();

        _chaseTimer += Time.deltaTime;
        if (hasBeenBound || _chaseTimer > Shadow.ChasePatternTime)
        {
            Shadow.PlusChaseCount();
            SoundManager.Instance.PlaySfxOnce(SfxName.Slime, idx: 2);
            hasBeenBound = false;
            if (Shadow.CurChaseCount > Shadow.SlowChaseCount)
            {
                //Logger.Log("고속 이동");
                Shadow.SetCollisionDamage(Shadow.DoneExpand
                    ? Shadow.ExpandCollisionDamage
                    : Shadow.FastCollisionDamage);
            }
            else
            {
                //Logger.Log("저속 이동");
                Shadow.SetMovementMultiplier(MovementType.Walk);
                Shadow.SetCollisionDamage(Shadow.SlowCollisionDamage);
            }
            ChangeState(IdleState);
            _chaseTimer = 0f;
        }
    }

    protected IEnumerator HandleExpandStateCoroutine()
    {
        Transform target = Shadow.transform;
        Vector3 startScale = target.localScale;
        Vector3 endScale = startScale * Shadow.MaxScale;
        float elapsed = 0f;

        SoundManager.Instance.PlaySfxOnce(SfxName.ShadowExpand);

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
