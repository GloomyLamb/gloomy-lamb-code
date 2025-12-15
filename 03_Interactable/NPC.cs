using UnityEngine;

public abstract class NPC : MonoBehaviour, IInteractable
{
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
}
