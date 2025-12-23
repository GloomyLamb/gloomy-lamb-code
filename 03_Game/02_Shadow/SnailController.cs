using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class SnailController : MonoBehaviour
{
    [Header("Target (드래그로 넣거나 Player 태그로 자동 탐색)")]
    public Transform target;

    [Header("Repath")]
    [SerializeField] private float repathInterval = 0.15f;  

    private NavMeshAgent agent;
    private float timer;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        // 인스펙터에 target 안 넣었으면 Player 태그로 자동 찾기
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null) target = player.transform;
        }
    }

    private void OnEnable()
    {
        timer = 0f;
        if (agent != null) agent.isStopped = false;
    }

    private void Update()
    {
        if (agent == null || target == null) return;

        timer += Time.deltaTime;
        if (timer >= repathInterval)
        {
            timer = 0f;
            agent.SetDestination(target.position);
        }
    }
}
