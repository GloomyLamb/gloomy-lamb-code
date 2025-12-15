/// <summary>
/// 상호작용 인터페이스
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// 상호작용 내부 로직
    /// </summary>
    public void Interact();

    /// <summary>
    /// 상호작용 키 팝업
    /// </summary>
    public virtual void PopUpKey()
    {
        // todo: 일반 키 넣고 필요 시 오버라이드
        Logger.Log("상호작용 키 팝업");
    }

    /// <summary>
    /// 상호작용 키 숨기기
    /// </summary>
    public virtual void HideKey()
    {
        Logger.Log("상호작용 키 숨기기");
    }
}