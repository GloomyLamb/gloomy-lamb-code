using UnityEngine;

public class SlimeShadowStateMachine : ShadowStateMachine
{
    public SlimeShadow Shadow { get; private set; }

    #region States
    // Ground
    public CommonShadowState WalkState { get; private set; }
    public CommonShadowState RunState { get; private set; }
    public CommonShadowState ExpandState { get; private set; }
    #endregion

    // 추적

    public SlimeShadowStateMachine(SlimeShadow shadow) : base(shadow)
    {
        Shadow = shadow;

        WalkState = new SlimeShadowWalkState(shadow, this);
        RunState = new SlimeShadowRunState(shadow, this);
        ExpandState = new SlimeShadowExpandState(shadow, this);
    }

    public override void Init()
    {
        base.Init();

        WalkState.Init(MovementType.Default, shadow.AnimationData.ChaseParameterHash, AnimType.Bool);
        RunState.Init(MovementType.Run, shadow.AnimationData.ChaseParameterHash, AnimType.Bool);
        ExpandState.Init(MovementType.Stop, shadow.AnimationData.ChaseParameterHash, AnimType.Bool);
    }

    protected override void Publish()
    {
        base.Publish();

        WalkState.OnFixedUpdate += HandleFixedUpdateChase;
        RunState.OnFixedUpdate += HandleFixedUpdateChase;
        ExpandState.OnFixedUpdate += HandleFixedUpdateChase;
    }

    private float _timer;
    private float _patternTime = 0.5f;

    protected override void HandleUpdateIdle()
    {
        if (Shadow.CurChaseCount == Shadow.ChaseCount + 1)
        {
            Logger.Log("확대 패턴 진입");
            ChangeState(ExpandState);
        }

        _timer += Time.deltaTime;
        if (shadow.Target != null)
        {
            if (_timer > _patternTime && !Shadow.IsFastMode)
            {
                Logger.Log("저속 이동");
                Shadow.IsFastMode = true;
                ChangeState(WalkState);
                _timer = 0f;
            }
            else if (_timer > _patternTime && Shadow.IsFastMode)
            {
                Logger.Log("고속 이동");
                ChangeState(RunState);
                _timer = 0f;
            }
        }
    }
}
