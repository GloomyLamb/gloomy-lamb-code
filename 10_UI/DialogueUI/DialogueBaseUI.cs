using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueBaseUI : BaseUI
{
    [Header("Input")]
    [SerializeField] protected InputActionAsset inputActionAsset;
    protected InputHandler input;
    
    [Header("Dialogue Text")]
    [SerializeField] protected TextMeshProUGUI dialogueText;



    protected const float DialogueSpeedRate = 10f;
    protected float dialogueSpeed = 1.0f;

    Coroutine dialogueCoroutine;


    public override void Init()
    {
        input = new InputHandler(inputActionAsset, InputType.DialogueBox);
        //
        // portraitPanels =  new List<DialoguePortraitPanel>();
        // for (int i = 0; i < portraitPanelOrigins; ++i)
        // {
        //     
        // }
    }

    private void Start()
    {
        InputManager.Instance.UseInput(input);
    }

    public virtual void ShowDialogue(DialogueData dialogueData)
    {
        this.gameObject.SetActive(true);
        //dialogueData.Name
    }

    public virtual void PrintDialogue(string dialogueString)
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
            yield return new WaitForSecondsRealtime(DialogueSpeedRate / dialogueSpeed);
            dialogueText.text += dialogueString[i];
        }

        dialogueCoroutine = null;
    }

}
