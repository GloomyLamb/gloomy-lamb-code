using UnityEngine;

public class DogShadowStateMachine : ShadowStateMachine
{
    public new DogShadow Shadow { get; private set; }

    #region States
    // Skill
    public ShadowState BiteState { get; private set; }
    public ShadowState BarkState { get; private set; }
    #endregion

    public DogShadowStateMachine(DogShadow shadow) : base(shadow)
    {
        Shadow = shadow;

        BiteState = new DogShadowBiteState(Shadow, this);
        BarkState = new DogShadowBarkState(Shadow, this);
    }

    public override void Init()
    {
        base.Init();
        BarkState.Init(MovementType.Stop, Shadow.AnimationData.IdleParameterHash, AnimType.Bool);
    }

    protected override void HandleUpdateChase()
    {
        base.HandleUpdateChase();

        Transform shadowT = Shadow.transform;
        Transform targetT = Shadow.Target.transform;

        if ((targetT.position - shadowT.position).sqrMagnitude < Shadow.SqrBiteRange)
        {
            ChangeState(BiteState);
        }
        else
        {
            // todo - bark 조건 보완
            ChangeState(BarkState);
        }
    }
}
