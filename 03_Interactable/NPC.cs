using UnityEngine;

public abstract class NPC : MonoBehaviour, IInteractable
{
    // NPC 기본 방향
    protected Vector3 forward;
    [SerializeField] protected float rotateSpeed = 5f;

    // 말풍선
    // todo: 프리팹 연결해서 자동 생성 및 관리

    // 상호작용 가능 관리

    // 플레이어 정보
    protected Transform player;

    private void Reset()
    {
        int layer = LayerMask.NameToLayer("Interactable");
        if (layer != -1)
            gameObject.layer = layer;
    }

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
    public void SetPlayer(Transform player)
    {
        this.player = player;
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
        // 플레이어 방향
        Vector3 dir = player.position - transform.position;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            rotateSpeed * Time.deltaTime);
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
}
