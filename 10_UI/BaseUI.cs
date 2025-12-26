using UnityEngine;

/// <summary>
/// UI 기본 클래스
/// </summary>
public abstract class BaseUI : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {

    }

    /// <summary>
    /// 외부 초기화
    /// </summary>
    public virtual void Setup()
    {

    }

    // 현재 불필요
    // void RegisterUI()
    // {
    // }
    //
    // private void OnDestroy()
    // {
    //     UnRegisterUI();
    // }
    //
    // void UnRegisterUI()
    // {
    //     
    // }

}
