/// <summary>
/// 그림자 기본 상태
/// </summary>
public class ShadowIdleState : ShadowState, ITransmutableState, IBindableState
{
    public ShadowIdleState(Shadow shadow, ShadowStateMachine stateMachine) : base(shadow, stateMachine)
    {
    }
}
