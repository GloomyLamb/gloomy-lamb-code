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

        BiteState = new DogShadowBiteState(Shadow, this);
        BackwardState = new DogShadowBackwardState(Shadow, this);
        BarkState = new DogShadowBarkState(Shadow, this);
    }

    public override void Init()
    {
        base.Init();
        BiteState.Init(MovementType.Stop, Shadow.SkillAnimationData.BiteParameterHash, AnimType.Bool, true);
        BackwardState.Init(MovementType.Walk, Shadow.AnimationData.ChaseParameterHash, AnimType.Bool, true);
        BarkState.Init(MovementType.Stop, Shadow.SkillAnimationData.BarkParameterHash, AnimType.Bool, true);
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
            ChangeState(BarkState);
        }
    }
}
