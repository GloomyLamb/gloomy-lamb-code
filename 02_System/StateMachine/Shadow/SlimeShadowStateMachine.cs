using UnityEngine;

public class SlimeShadowStateMachine : ShadowStateMachine
{
    public new SlimeShadow Shadow { get; private set; }

    #region States
    // Ground
    public ShadowState WalkState { get; private set; }
    public ShadowState RunState { get; private set; }
    public ShadowState ExpandState { get; private set; }
    #endregion

    // 추적

    public SlimeShadowStateMachine(SlimeShadow shadow) : base(shadow)
    {
        Shadow = shadow;

        WalkState = new SlimeShadowWalkState(shadow, this);
        RunState = new SlimeShadowChaseState(shadow, this);
        ExpandState = new SlimeShadowExpandState(shadow, this);
    }

    public override void Init()
    {
        base.Init();

        WalkState.Init(MovementType.Default, Shadow.AnimationData.ChaseParameterHash, AnimType.Bool, true);
        RunState.Init(MovementType.Run, Shadow.AnimationData.ChaseParameterHash, AnimType.Bool, true);
        ExpandState.Init(MovementType.Stop, Shadow.AnimationData.ChaseParameterHash, AnimType.Bool, true);
    }

    public override void Register()
    {
        base.Register();

        WalkState.OnFixedUpdate += HandleFixedUpdateChase;
        RunState.OnFixedUpdate += HandleFixedUpdateChase;
        ExpandState.OnFixedUpdate += HandleFixedUpdateChase;
    }

    public override void UnRegister()
    {
        base.UnRegister();

        WalkState.OnFixedUpdate -= HandleFixedUpdateChase;
        RunState.OnFixedUpdate -= HandleFixedUpdateChase;
        ExpandState.OnFixedUpdate -= HandleFixedUpdateChase;
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
        if (Shadow.Target != null)
        {
            if (_timer > _patternTime && !Shadow.IsFastMode)
            {
                Shadow.IsFastMode = true;
                ChangeState(WalkState);
                _timer = 0f;
            }
            else if (_timer > _patternTime && Shadow.IsFastMode)
            {
                ChangeState(RunState);
                _timer = 0f;
            }
        }
    }
}
