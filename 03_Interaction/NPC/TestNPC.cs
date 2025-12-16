using UnityEngine;
using UnityEngine.UI;

public class TestNPC : NPCBase
{
    [SerializeField] private Canvas _cachedCanvas;

    public override void Interact()
    {
        Logger.Log("NPC와 상호작용");
        if (dialogueUI != null)
        {
            Logger.LogWarning("이미 대화 UI가 존재합니다.");
            return;
        }

        if (_cachedCanvas == null)
        {
            GameObject go = new("Canvas");

            _cachedCanvas = go.AddComponent<Canvas>();
            _cachedCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

            var scaler = go.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);

            go.AddComponent<GraphicRaycaster>();
        }

        dialogueUI = Instantiate(dialogueUIPrefab, _cachedCanvas.transform);

        var rt = dialogueUI.GetComponent<RectTransform>();
        rt.anchoredPosition = Vector2.zero;
        rt.localScale = Vector3.one;
        rt.localRotation = Quaternion.identity;
    }
}
