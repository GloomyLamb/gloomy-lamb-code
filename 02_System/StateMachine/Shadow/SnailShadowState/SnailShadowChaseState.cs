using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SnailShadowChaseState : SnailShadowGroundState
{
    private readonly SnailShadowStateMachine snailSM;
    private readonly NavMeshAgent agent;

    private float nextUpdateTime;
    private const float updateInterval = 0.1f;
    public SnailShadowChaseState(ShadowStateMachine stateMachine) : base(stateMachine)
    {
        snailSM = (SnailShadowStateMachine)stateMachine; // 상태 머신 캐스팅
        agent = GetAgentSafely(); // NavMeshAgent 컴포넌트 가져오기
    }

    public override void Enter()
    {
        base.Enter();
        // idle 움직임 애니메이션 시작
        if (agent != null)
        {
            agent.isStopped = false;
        }

        nextUpdateTime = 0f;
    }
    public override void Update()
    {
        base.Update();

        //  타겟이 사라졌으면 Idle로 복귀
        if (snailSM.Target == null)
        {
            if (agent != null) agent.ResetPath();
            snailSM.ChangeState(snailSM.IdleState);
            return;
        }

        //  NavMesh 추적(목적지 갱신)
        if (agent == null) return;

        if (Time.time >= nextUpdateTime)
        {
            agent.SetDestination(snailSM.Target.position);
            nextUpdateTime = Time.time + updateInterval;
        }
    }

    public override void Exit()
    {
        base.Exit();
        // idle 움직임 애니메이션 종료
    }
    private NavMeshAgent GetAgentSafely()
    {

        var shadowObj = Object.FindObjectOfType<Shadow>();
        if (shadowObj != null)
            return shadowObj.GetComponent<NavMeshAgent>();

        return null;
    }
}
