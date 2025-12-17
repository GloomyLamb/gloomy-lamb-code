using UnityEngine;
using UnityEngine.InputSystem;

// todo : Player 구조 다같이 이야기 해보기
// controller 로 빼도 되지만, 우리 Player 들이 생각보다 가벼울 것임
public abstract class Player : MonoBehaviour
{
    [Header("SO Data")]
    [SerializeField] protected InteractionRangeData interactionRangeData;

    // 컴포넌트
    protected Interaction interaction;

    public Vector3 Forward => forward;
    protected Vector3 forward;

    private void Awake()
    {
        forward = transform.forward;
        Init();
    }

    protected abstract void Init();

    protected virtual void Update()
    {
        interaction?.UpdateInteractionTarget();
    }

    private void OnDestroy()
    {
    }

    #region 컴포넌트 연결

    /// <summary>
    /// 상호작용 컴포넌트 설정 및 연결
    /// </summary>
    protected void SetupInteractionComponent()
    {
        interaction = gameObject.AddComponent<Interaction>();
        interaction.Init(interactionRangeData);
    }

    #endregion
}