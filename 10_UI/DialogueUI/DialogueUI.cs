using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DialogueUI : BaseUI
{
    [Header("Input")]
    [SerializeField] private InputActionAsset inputActionAsset;
    
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI dialogueText;

    [Space]
    [SerializeField] private GameObject characterNamePanel;
    [SerializeField] private TextMeshProUGUI characterNameText;
    
    
    

    const float DialogueSpeedRate = 10f;
    private float dialogueSpeed = 1.0f;

    InputHandler input;

    Coroutine dialogueCoroutine;
    public override void Init()
    {
        input = new InputHandler(inputActionAsset, InputType.DialogueBox);
        
    }

    private void Start()
    {
        InputManager.Instance.UseInput(input);
    }

    public void ShowDialogue(DialogueData dialogueData)
    {
        this.gameObject.SetActive(true);
        //dialogueData.Name
    }
    
    public void PrintDialogue(string dialogueString)
    {
        if (dialogueCoroutine != null)
        {
            StopCoroutine(dialogueCoroutine);
        }
        
        dialogueSpeed = dialogueSpeed <= 0 ? 1.0f : dialogueSpeed;
        
        dialogueCoroutine = StartCoroutine(PrintDialogueRoutine(dialogueString));
    }

    IEnumerator PrintDialogueRoutine(string dialogueString)
    {
        dialogueText.text = "";
        //int nowPrintCount = 0;
        
        for (int i = 0; i < dialogueString.Length; i++)
        {
            yield return new WaitForSecondsRealtime(DialogueSpeedRate / dialogueSpeed);
            dialogueText.text += dialogueString[i];
        }

        dialogueCoroutine = null;
    }

}
