using UnityEngine;
using UnityEngine.AI;

public abstract class ShadowController : MonoBehaviour
{
    // todo: 각종 스텟
    protected Shadow curShadow;

    [field: Header("추격")]
    [SerializeField] private NavMeshAgent _agent;
    [field: SerializeField] public Transform Target { get; private set; }

    private float _agentTimer;
    private const float _updateInterval = 0.1f;

    protected virtual void Start()
    {
        if (Target == null)
        {
            Target = GameManager.Instance.Player.transform;
        }
    }

    #region 추격
    protected void HandleMove()
    {
        if (Target == null)
        {
            Logger.Log("타겟 없음");
            _agent.ResetPath();
        }

        _agentTimer += Time.deltaTime;
        if (_agentTimer > _updateInterval)
        {
            _agent.SetDestination(Target.position);
            _agentTimer = 0f;
        }
    }

    public void SetAgentMovementModifier(float modifier)
    {
        _agent.speed = curShadow.MovementSpeed * modifier;
    }

    private NavMeshAgent GetAgentSafely()
    {
        var shadowObj = Object.FindObjectOfType<Shadow>();
        if (shadowObj != null)
            return shadowObj.GetComponent<NavMeshAgent>();

        return null;
    }
    #endregion

    #region 에디터 전용
#if UNITY_EDITOR
    protected virtual void Reset()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
#endif
    #endregion
}
