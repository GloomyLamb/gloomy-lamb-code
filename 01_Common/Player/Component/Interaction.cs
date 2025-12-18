using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어 컴포넌트로 활용될 상호작용 시스템
/// </summary>
public class Interaction : MonoBehaviour
{
    [Header("SO Data")]
    [SerializeField] protected InteractionRangeData interactionRangeData;

    private float _interactRange;
    private float _interactAngle;
    private LayerMask _interactableLayer;

    private bool _useAngleWeight = false;
    private bool _useDistanceWieght = true;

    private Vector3 Forward => transform.forward;

    // 캐싱
    private IInteractable _curInteractable;

    #region 초기화
    private void Awake()
    {
        Init(interactionRangeData);
    }

    public void Init(InteractionRangeData data)
    {
        _interactRange = data.InteractRange;
        _interactAngle = data.InteractAngle;
        _interactableLayer = data.InteractableLayer;
        _useAngleWeight = data.UseAngleWeight;
        _useDistanceWieght = data.UseDistanceWieght;
        BindInput();
    }

    /// <summary>
    /// input handler에 interact 이벤트 바인딩
    /// </summary>
    /// <param name="input"></param>
    private void BindInput()
    {
        InputManager.Instance.BindInputEvent(
            InputType.Player,
            InputMapName.Default,
            InputActionName.Interaction,
            OnInteract);
    }

    /// <summary>
    /// input handler - player의 interact 이벤트 콜백
    /// </summary>
    /// <param name="context"></param>
    private void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            _curInteractable?.Interact();
        }
    }
    #endregion

    #region Unity API
    private void Update()
    {
        UpdateInteractionTarget();
    }
    #endregion

    #region 상호작용 오브젝트 탐색
    /// <summary>
    /// [public] interactable 오브젝트 탐색
    /// </summary>
    public void UpdateInteractionTarget()
    {
        Collider[] cols = GetInteractables();                       // 1. 반경 내 오브젝트 탐색
        if (cols.Length == 0) return;

        IInteractable interactable = FindBestInteractable(cols);    // 2. 우선순위 계산 (각도 + 거리)

        // 범위에 상호작용 가능한 오브젝트가 없으면 상태 초기화
        if (_curInteractable != null && interactable == null)
        {
            Logger.Log("상호작용 캐싱 초기화");
            _curInteractable.HideInteractUI();
            _curInteractable = null;
            return;
        }

        // 변경 없으면 패스
        if (_curInteractable == interactable) return;

        interactable?.HideInteractUI();
        _curInteractable = interactable;
        interactable?.ShowInteractUI();
    }

    /// <summary>
    /// 반경 내 오브젝트 탐색
    /// </summary>
    private Collider[] GetInteractables()
    {
        return Physics.OverlapSphere(
           transform.position,
           _interactRange,
           _interactableLayer);
    }

    /// <summary>
    /// 우선 순위 계산 (각도 + 거리)
    /// </summary>
    /// <param name="cols"></param>
    /// <returns></returns>
    private IInteractable FindBestInteractable(Collider[] cols)
    {
        IInteractable best = null;
        float bestScore = float.MaxValue;
        float cosThreshold = Mathf.Cos(Mathf.Deg2Rad * (_interactAngle / 2));

        // 현재 기준: 거리
        foreach (var col in cols)
        {
            if (!col.TryGetComponent<IInteractable>(out var interactable)) continue;

            // 거리 계산
            Vector3 dir = col.transform.position - transform.position;
            float sqrDistance = dir.sqrMagnitude;
            if (sqrDistance < 0.0001f) continue;

            // 각도 계산
            float dot = Vector3.Dot(Forward, dir) / Mathf.Sqrt(sqrDistance);
            if (dot < cosThreshold) continue;

            // 각도 + 거리 가중치 합산 (점수가 낮을수록 우선순위 높음)
            float score =
                (1f - dot) * (_useAngleWeight ? 1 : 0)
                + sqrDistance * (_useDistanceWieght ? 0.5f : 0);

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
        Gizmos.DrawWireSphere(transform.position, _interactRange);

        Vector3 origin = transform.position;
        Vector3 fwd = Forward;

        // 직선
        Vector3 left = Quaternion.AngleAxis(-_interactAngle * 0.5f, Vector3.up) * fwd;
        Vector3 right = Quaternion.AngleAxis(_interactAngle * 0.5f, Vector3.up) * fwd;

        Gizmos.DrawLine(origin, origin + left * _interactRange);
        Gizmos.DrawLine(origin, origin + right * _interactRange);
    }
    #endregion

    #region 에디터 전용
#if UNITY_EDITOR
    private void Reset()
    {
        if (interactionRangeData == null)
        {
            interactionRangeData = AssetLoader.FindAndLoadByName<InteractionRangeData>("InteractionRangeData");
        }
    }
#endif
    #endregion
}
