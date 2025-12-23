using UnityEngine;
/// <summary>
/// 그림자 - 달팽이 기본 움직임
/// </summary>
public class SnailShadowIdleState : SnailShadowGroundState        // 목표 감지 로직 포함
{
    private readonly SnailShadowStateMachine snailSM;
    public SnailShadowIdleState(StateMachine stateMachine) : base(stateMachine)
    {
        snailSM = (SnailShadowStateMachine)stateMachine; // 상태 머신 캐스팅
    }

    public override void Enter()
    {
        base.Enter();
        // idle 움직임 애니메이션 시작
    }
    public override void Update()
    {
        Transform target = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (target == null)

            return;
        snailSM.Target = target;  // 타겟 설정
        snailSM.ChangeState(snailSM.ChaseState); // ChaseState로 전환시도 
    }


    public override void Exit()
    {
        base.Exit();
        // idle 움직임 애니메이션 종료
    }
}
