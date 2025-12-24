using UnityEngine;

public class DogShadowStateMachine : ShadowStateMachine
{
    public new DogShadow Shadow { get; private set; }

    #region States
    // Skill
    public IState BiteState { get; private set; }
    public IState BarkState { get; private set; }
    #endregion

    public DogShadowStateMachine(DogShadow shadow) : base(shadow)
    {
        Shadow = shadow;

        BiteState = new DogShadowBiteState(this);
        BarkState = new DogShadowBarkState(this);
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
