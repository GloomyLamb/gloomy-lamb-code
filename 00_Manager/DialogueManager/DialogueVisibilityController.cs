using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

/// <summary>
/// SetActive가 아니라 모델 Renderer 만 껐다 켰다 할 수 있게 
/// </summary>
public class DialogueVisibilityController : MonoBehaviour
{
    Renderer[] renderers;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }


    private void Start()
    {
        DialogueManager.Instance.OnDialogueStartAction += Hide;
        DialogueManager.Instance.OnDialogueEndAction += Show;
    }

    public void Show()
    {
        if (renderers != null)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].enabled = true;
            }
        }
    }

    public void Hide()
    {
        if (renderers != null)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].enabled = false;
            }
        }
    }

    private void OnDestroy()
    {
        DialogueManager.Instance.OnDialogueStartAction -= Show;
        DialogueManager.Instance.OnDialogueEndAction -= Hide;
    }
}