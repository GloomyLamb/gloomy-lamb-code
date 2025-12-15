using UnityEngine;
using UnityEngine.InputSystem;

// todo : Player 구조 다같이 이야기 해보기
// controller 로 빼도 되지만, 우리 Player 들이 생각보다 가벼울 것임
public abstract class Player : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] protected InputActionAsset inputAction;
    protected InputHandler input;

    [Header("Pivot")]
    [SerializeField] protected Transform pivot;

    [Header("Interaction Setting")]
    [SerializeField] protected float interactRange = 3f;
    [SerializeField] protected float interactAngle = 60f;
    [SerializeField] protected LayerMask interactableLayer;

    // 가중치
    [SerializeField] protected bool angleWeight = false;
    [SerializeField] protected bool distanceWieght = true;
    private float cosThreshold;

    public Vector3 Forward => forward;
    protected Vector3 forward;

    private void Awake()
    {
        input = new InputHandler(inputAction, InputType.Player);
        forward = transform.forward;
        cosThreshold = Mathf.Cos(Mathf.Deg2Rad * (interactAngle / 2));
        Init();
    }

    protected abstract void Init();

    protected virtual void Update()
    {
        OnDetectInteractablesEnter();
    }

    private void OnDestroy()
    {
        input?.DisposeInputEvent();
    }

    #region 상호작용
    protected void OnDetectInteractablesEnter()
    {
        Collider[] cols = DetectInteractable();                     // 1. 반경 내 오브젝트 탐색
        IInteractable interactable = FindBestInteractable(cols);    // 2. 우선순위 계산 (각도 + 거리)
        interactable?.PopUpKey();                                   // 3. 타겟 확정 -> 팝업 표시
    }

    /// <summary>
    /// 반경 내 오브젝트 탐색
    /// </summary>
    protected virtual Collider[] DetectInteractable()
    {
        return Physics.OverlapSphere(
           transform.position,
           interactRange,
           interactableLayer);
    }

    /// <summary>
    /// 우선 순위 계산 (각도 + 거리)
    /// </summary>
    /// <param name="cols"></param>
    /// <returns></returns>
    protected virtual IInteractable FindBestInteractable(Collider[] cols)
    {
        IInteractable best = null;
        float bestScore = float.MaxValue;

        // 현재 기준: 거리
        foreach (var col in cols)
        {
            if (!col.TryGetComponent<IInteractable>(out var interactable)) continue;

            // 거리 계산
            Vector3 dir = col.transform.position - transform.position;
            float sqrDistance = dir.sqrMagnitude;
            if (sqrDistance < 0.0001f) continue;

            // 각도 계산
            float dot = Vector3.Dot(forward, dir) / Mathf.Sqrt(sqrDistance);
            if (dot < cosThreshold) continue;

            // 각도 + 거리 가중치 합산 (점수가 낮을수록 우선순위 높음)
            float score =
                (1f - dot) * (angleWeight ? 1 : 0)
                + sqrDistance * (distanceWieght ? 1 : 0);

            if (score < bestScore)
            {
                bestScore = score;
                best = interactable;
            }
        }

        return best;
    }

    /// <summary>
    /// 탐색 범위 표시
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRange);

        Vector3 origin = transform.position;
        Vector3 fwd = forward;

        // 직선
        Vector3 left = Quaternion.AngleAxis(-interactAngle * 0.5f, Vector3.up) * fwd;
        Vector3 right = Quaternion.AngleAxis(interactAngle * 0.5f, Vector3.up) * fwd;

        Gizmos.DrawLine(origin, origin + left * interactRange);
        Gizmos.DrawLine(origin, origin + right * interactRange);
    }

    #endregion
}
