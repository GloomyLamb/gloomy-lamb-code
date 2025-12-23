using UnityEngine.Events;

/// <summary>
/// 더스키 상태 베이스 클래스
/// </summary>
public abstract class BaseDuskyState : BaseMoveableState
{
    protected DuskyPlayer player;
    
    public BaseDuskyState(MoveableStateMachine stateMachine, DuskyPlayer player) : base(stateMachine)
    {
        this.player = player;
    }

}
