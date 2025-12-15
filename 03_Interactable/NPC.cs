using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class NPC : MonoBehaviour, IInteractable
{
    // NPC 기본 방향
    protected Vector3 forward;
    [SerializeField] protected float rotateSpeed = 270f;

    // 말풍선
    // todo: 프리팹 연결해서 자동 생성 및 관리

    // 상호작용 가능 관리

    // 플레이어 정보
    protected Transform player;

    private void Awake()
    {
        this.forward = transform.forward;
    }

    protected virtual void Update()
    {
        if (player != null)
        {
            RotateToPlayer();
        }
    }

    #region 플레이어 상호작용
    /// <summary>
    /// 플레이어 캐싱
    /// </summary>
    /// <param name="player"></param>
    public void SetPlayer(Player player)
    {
        this.player = player.transform;
    }

    /// <summary>
    /// 플레이어 캐싱 제거
    /// </summary>
    public void ResetPlayer()
    {
        this.player = null;
    }

    /// <summary>
    /// 플레이어 방향대로 회전
    /// </summary>
    private void RotateToPlayer()
    {
        Vector3 dir = player.position - transform.position; // 플레이어 방향
        dir.y = 0f;                                         // 수평 방향만 고려

        if (dir.sqrMagnitude < 0.0001f) return;             // 너무 가까우면 회전 안 함

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            rotateSpeed * Time.deltaTime);
    }
    #endregion

    #region 트리거 감지
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            Logger.Log("플레이어 감지");
            SetPlayer(player);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            Logger.Log("플레이어 감지 해제");
            if (this.player == player.transform)
            {
                ResetPlayer();
            }
        }
    }
    #endregion

    #region IInteractable 구현
    public abstract void Interact();

    /// <summary>
    /// 상호작용 키 보여주기
    /// </summary>
    public virtual void ShowInteractUI()
    {
        // todo: 일반 키 넣고 필요 시 오버라이드
        Logger.Log("상호작용 키 보여주기");
    }

    /// <summary>
    /// 상호작용 키 숨기기
    /// </summary>
    public virtual void HideInteractUI()
    {
        Logger.Log("상호작용 키 숨기기");
    }
    #endregion

    #region 에디터 전용
#if UNITY_EDITOR
    private void Reset()
    {
        ApplyLayer();
        ApplyCollider();
    }

#endif
    private void ApplyLayer()
    {
        int layer = LayerMask.NameToLayer("Interactable");
        if (layer != -1)
            gameObject.layer = layer;
    }

    private void ApplyCollider()
    {
        var col = GetComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = 3f;
        col.center = Vector3.zero;
    }
    #endregion
}
