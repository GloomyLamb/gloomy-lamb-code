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

    // todo: 테스트 후 SO로 분리 고려
    [Header("Interaction Setting")]
    [SerializeField] protected float interactRange = 3f;
    [SerializeField][Range(60f, 180f)] protected float interactAngle = 120f;
    [SerializeField] protected LayerMask interactableLayer;
    [SerializeField] protected bool useAngleWeight = false;
    [SerializeField] protected bool useDistanceWieght = true;

    // 컴포넌트
    protected Interaction interaction;

    public Vector3 Forward => forward;
    protected Vector3 forward;

    private void Awake()
    {
        input = new InputHandler(inputAction, InputType.Player);
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
        input?.DisposeInputEvent();
    }

    #region 컴포넌트 연결
    /// <summary>
    /// 상호작용 컴포넌트 설정 및 연결
    /// </summary>
    protected void SetupInteractionComponent()
    {
        interaction = gameObject.AddComponent<Interaction>();
        interaction.Init(
            interactRange,
            interactAngle,
            interactableLayer,
            useAngleWeight,
            useDistanceWieght);
        interaction.BindInput(input);
    }
    #endregion
}
