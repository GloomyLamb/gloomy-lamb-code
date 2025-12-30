using TMPro;
using UnityEngine;

public class PopUpUI : MonoBehaviour
{
    [SerializeField] private GameObject _popUpPrefab;
    private GameObject _popUpGo;
    private TextMeshProUGUI _popUpText;
    private const string _defaultText = "현재 지원하지 않는 기능입니다.";

    private void Init()
    {
        GameObject go = Instantiate(_popUpPrefab);
        _popUpGo = go.transform.FindChild<RectTransform>("PopUp").gameObject;
        _popUpText = go.transform.FindChild<TextMeshProUGUI>("Text (TMP)");
    }

    public void ShowPopUp(string text = null)
    {
        if (_popUpGo == null)
        {
            Init();
        }
        _popUpGo.SetActive(true);
        _popUpText.text = string.IsNullOrEmpty(text) ? _defaultText : text;
    }

#if UNITY_EDITOR
    private void Reset()
    {
        _popUpPrefab = AssetLoader.FindAndLoadByName("Canvas - PopUp");
    }
#endif
}
