using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueBaseUI : BaseUI
{
    [Header("Dialogue Text")] [SerializeField]
    protected TextMeshProUGUI dialogueText;

    protected const float DialogueSpeedRate = 10f;
    [SerializeField] protected float dialogueSpeed = 1.0f;

    DialogueData nowDialogueData;
    Coroutine dialogueCoroutine;
    public bool IsPrinting => dialogueCoroutine != null;


    public override void Init()
    {
    }


    public void NextDialogue(DialogueData dialogueData)
    {
        nowDialogueData = dialogueData;
        NextDialogueInteranl(nowDialogueData);
        StartPrintDialogueRoutine(nowDialogueData.Dialogue);
        //dialogueData.Name
    }

    protected virtual void NextDialogueInteranl(DialogueData dialogueData)
    {
    }


    // 재정의할 내용이 있을까?
    public virtual void StopDialogue()
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
            dialogueCoroutine = null;
        }
    }

    public void ShowDialogueImmediately()
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
            dialogueCoroutine = null;
        }

        if (nowDialogueData == null) return;

        dialogueText.text = nowDialogueData.Dialogue;
    }

    public void StartPrintDialogueRoutine(string dialogueString)
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }

        dialogueSpeed = dialogueSpeed <= 0 ? 1.0f : dialogueSpeed;
        dialogueCoroutine = StartCoroutine(PrintDialogueRoutine(dialogueString));
    }

    protected IEnumerator PrintDialogueRoutine(string dialogueString)
    {
        dialogueText.text = "";
        //int nowPrintCount = 0;

        for (int i = 0; i < dialogueString.Length; i++)
        {
            yield return new WaitForSecondsRealtime((1 / DialogueSpeedRate) / dialogueSpeed);
            dialogueText.text += dialogueString[i];
        }

        dialogueCoroutine = null;
    }
}