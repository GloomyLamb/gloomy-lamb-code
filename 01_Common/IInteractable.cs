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
    /// 상호작용 키 보여주기
    /// </summary>
    public void ShowInteractUI();

    /// <summary>
    /// 상호작용 키 숨기기
    /// </summary>
    public void HideInteractUI();
}