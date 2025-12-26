using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI - 타이틀
/// </summary>
public class TitleUI : BaseUI
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Button _exitButton;

    protected override void Init()
    {
        _startButton.onClick.AddListener(OnStartGame);
        _settingButton.onClick.AddListener(OnPopUpSettingWindow);
        _exitButton.onClick.AddListener(OnExitGame);
    }

    private void OnDestroy()
    {
        _startButton.onClick.RemoveAllListeners();
        _settingButton.onClick.RemoveAllListeners();
        _exitButton.onClick.RemoveAllListeners();
    }

    private void OnStartGame()
    {
        GameManager.Instance.ShowVideo(VideoID.Test, SceneType.LibraryScene);
    }

    private void OnPopUpSettingWindow()
    {
        Logger.Log("todo: 세팅창 열기");
    }

    private void OnExitGame()
    {
        // todo: 데이터 저장
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    #region 에디터 전용
#if UNITY_EDITOR
    private void Reset()
    {
        _startButton = transform.FindChild<Button>("Button -  New Game");
        _settingButton = transform.FindChild<Button>("Button -  Settings");
        _exitButton = transform.FindChild<Button>("Button -  Exit");
    }
#endif
    #endregion
}
