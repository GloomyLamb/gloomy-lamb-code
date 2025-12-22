using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SaveUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text messageText;

    [Header("Timing")]
    [SerializeField] private float fadeInTime = 0.15f;
    [SerializeField] private float showTime = 1.0f;
    [SerializeField] private float fadeOutTime = 0.25f;

    private Coroutine routine;

    private void Reset()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        messageText = GetComponentInChildren<TMP_Text>();
    }

    private void Awake()
    {
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        if (messageText == null) messageText = GetComponentInChildren<TMP_Text>();

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void Show(string msg)
    {
        gameObject.SetActive(true);
        messageText.text = msg;

        if (routine != null) StopCoroutine(routine);
        routine = StartCoroutine(ToastRoutine());
    }

    private IEnumerator ToastRoutine()
    {
       

        yield return Fade(0f, 1f, fadeInTime);

        yield return new WaitForSecondsRealtime(showTime);

        yield return Fade(1f, 0f, fadeOutTime);

        gameObject.SetActive(false);
        routine = null;
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        if (duration <= 0f)
        {
            canvasGroup.alpha = to;
            yield break;
        }

        float t = 0f;
        canvasGroup.alpha = from;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float a = Mathf.Clamp01(t / duration);
            canvasGroup.alpha = Mathf.Lerp(from, to, a);
            yield return null;
        }

        canvasGroup.alpha = to;
    }
}
