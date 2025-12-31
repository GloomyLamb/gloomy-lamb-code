using System;
using UnityEngine;

public abstract class BaseInteractableObject : MonoBehaviour, IInteractable
{
    public abstract void Interact();
    
    InteractionMark _interactionMark;
    [SerializeField] InteractionMarkType _interactionType;

    private void Awake()
    {
        _interactionMark = GetComponentInChildren<InteractionMark>(true);
        _interactionMark?.Hide();
    }

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

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            _interactionMark?.Show(_interactionType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            _interactionMark?.Hide();
        }
    }
}
