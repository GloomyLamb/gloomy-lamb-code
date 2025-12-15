using UnityEditor;
using UnityEngine;

/// <summary>
/// NPC 기본 클래스
/// </summary>
[RequireComponent(typeof(SphereCollider))]
public abstract class NPC : MonoBehaviour, IInteractable
{
    [Header("테스트")]
    [SerializeField] protected bool isTest = false;

    [Header("NPC 기본 설정")]
    [SerializeField][Range(90f, 360f)] protected float rotateSpeed = 270f;
    protected Vector3 forward;      // NPC 기본 방향

    // 말풍선
    [SerializeField] protected GameObject speechBubblePrefab;
    protected GameObject speechBubble;

    // 상호작용 가능 관리

    // 플레이어 정보
    protected Transform player;

    private void Awake()
    {
        this.forward = transform.forward;
        SpawnSpeechBubble();
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

    #region 말풍선
    /// <summary>
    /// 게임 시작 시 NPC 말풍선 생성하기
    /// </summary>
    private void SpawnSpeechBubble()
    {
        if (speechBubblePrefab == null)
        {
            Logger.LogWarning("말풍선 프리팹 없음");
            return;
        }
        speechBubble = Instantiate(speechBubblePrefab, transform);
        speechBubble.transform.localPosition = new Vector3(0f, 1.7f, 0f);
        speechBubble.SetActive(isTest);
    }

    /// <summary>
    /// 말풍선 ON/OFF 토글
    /// </summary>
    protected void ToggleSpeechBubble()
    {
        if (speechBubble == null) return;
        speechBubble.SetActive(!speechBubble.activeSelf);
    }
    #endregion

    #region 테스트
    public void Test_ToggleSpeechBubble()
    {
        ToggleSpeechBubble();
    }

    public void Test_SpawnSpeechBubbleDefault()
    {
        Destroy(speechBubble.gameObject);
        ApplySpeechBubble("SpeechBubble_Default");
        SpawnSpeechBubble();
    }

    public void Test_SpawnSpeechBubbleUI()
    {
        Destroy(speechBubble.gameObject);
        ApplySpeechBubble("SpeechBubble_UI");
        SpawnSpeechBubble();
    }
    #endregion

    #region 에디터 전용
#if UNITY_EDITOR
    private void Reset()
    {
        ApplyLayer();
        ApplyCollider();
        ApplySpeechBubble("SpeechBubble_Default");
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

    private void ApplySpeechBubble(string name)
    {
        if (speechBubblePrefab != null) return;

        string[] guids = AssetDatabase.FindAssets($"t:Prefab {name}");

        if (guids.Length == 0)
        {
            Logger.Log("SpeechBubble 프리팹 못 찾음");
            return;
        }

        string path = AssetDatabase.GUIDToAssetPath(guids[0]);
        speechBubblePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
    }
    #endregion
}
