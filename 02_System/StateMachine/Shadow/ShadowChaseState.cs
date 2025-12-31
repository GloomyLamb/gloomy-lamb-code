/// <summary>
/// 그림자 추격 상태
/// </summary>
public class ShadowChaseState : ShadowState, ITransmutableState, IBindableState
{
    public ShadowChaseState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }
}
