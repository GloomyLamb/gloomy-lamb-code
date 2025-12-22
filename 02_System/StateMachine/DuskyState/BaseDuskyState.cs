using UnityEngine.Events;

/// <summary>
/// 더스키 상태 베이스 클래스
/// </summary>
public class BaseDuskyState : BaseMoveableState
{
    protected DuskyPlayer _player;
    
    public BaseDuskyState(MoveableStateMachine stateMachine, DuskyPlayer player) : base(stateMachine)
    {
        _player = player;
    }
}
