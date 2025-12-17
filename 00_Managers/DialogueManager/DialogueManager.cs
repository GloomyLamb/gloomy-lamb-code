using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class DialogueManager : GlobalSingletonManager<DialogueManager>
{
    [Header("Input")] [SerializeField] protected InputActionAsset inputActionAsset;
    protected InputHandler input;

    // 얘네를 어떻게 정리해둬야할지... 고민...
    [SerializeField] TalkDialogueUI talkDialogueUI;


    // 현재 플레이중인 대화, 진행도
    private DialogueBaseUI nowDialogueUI;
    DialogueType nowDialogueType;
    private List<DialogueData> nowPlayingDialogue;
    int nowDialogueIndex = 0;

    // 누가 들고 있어줘야하는가...? 
    private Dictionary<InputType, bool> prevInputState = new Dictionary<InputType, bool>();

    public event Action OnDialogueStartAction;
    public event Action OnNextDialogueAction;
    public event Action OnDialogueEndAction;


    protected override void Init()
    {
        input = new InputHandler(inputActionAsset, InputType.DialogueBox);
        input.BindInputEvent(InputMapName.Default, InputActionName.Next, OnNextDialogue);

        talkDialogueUI?.Setup();
    }

    private void Start()
    {
        InputManager.Instance.UseInput(input);
        InputManager.Instance.LockInput(InputType.DialogueBox);
    }


    public void StartDialogue(DialogueAsset dialogueAsset)
    {
        if (dialogueAsset == null) return;

        prevInputState = InputManager.Instance.SoloInput(InputType.DialogueBox);

        nowDialogueIndex = 0;
        nowDialogueType = dialogueAsset.DialogueType;

        nowPlayingDialogue = dialogueAsset.DialogueList;

        ActiveDialogueBox(dialogueAsset.DialogueType);
        NextDialogue();

        OnDialogueStartAction?.Invoke();
    }

    public void StartDialogue(DialogueType dialogueType, string dialogueString)
    {
        prevInputState = InputManager.Instance.SoloInput(InputType.DialogueBox);

        nowDialogueIndex = 0;
        nowDialogueType = dialogueType;

        List<DialogueData> newDialogueDataList = new List<DialogueData>();
        DialogueData newDialogueData = new DialogueData(dialogueString);
        newDialogueDataList.Add(newDialogueData);

        nowPlayingDialogue = newDialogueDataList;
        ActiveDialogueBox(dialogueType);
        NextDialogue();

        OnDialogueEndAction?.Invoke();
    }

    public void StartDialogue(DialogueType dialogueType, List<string> dialogueList)
    {
        prevInputState = InputManager.Instance.SoloInput(InputType.DialogueBox);

        nowDialogueIndex = 0;
        nowDialogueType = dialogueType;

        List<DialogueData> newDialogueDataList = new List<DialogueData>();
        for (int i = 0; i < dialogueList.Count; i++)
        {
            DialogueData newDialogueData = new DialogueData(dialogueList[i]);
            newDialogueDataList.Add(newDialogueData);
        }

        nowPlayingDialogue = newDialogueDataList;
        ActiveDialogueBox(dialogueType);
        NextDialogue();

        OnDialogueEndAction?.Invoke();
    }

    void OnNextDialogue(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            NextDialogue();
        }
    }

    void ActiveDialogueBox(DialogueType dialogueType)
    {
        switch (dialogueType)
        {
            case DialogueType.Default:
                nowDialogueUI = talkDialogueUI;
                break;
            case DialogueType.Story:
                break;
            case DialogueType.Bubble:
                break;
        }

        nowDialogueUI?.gameObject.SetActive(true);
    }

    public void NextDialogue()
    {
        if (nowPlayingDialogue == null) return;
        if (nowDialogueIndex >= nowPlayingDialogue.Count)
        {
            EndDialogue();
        }
        else
        {
            if (nowDialogueUI != null)
            {
                if (nowDialogueUI.IsPrinting)
                {
                    nowDialogueUI.ShowDialogueImmediately();
                    return;
                }

                bool lastDialogue = nowDialogueIndex == nowPlayingDialogue.Count - 1;
                nowDialogueUI?.NextDialogue(nowPlayingDialogue[nowDialogueIndex], lastDialogue);
            }

            OnNextDialogueAction?.Invoke();
            nowDialogueIndex++;
        }
    }


    public void EndDialogue()
    {
        InputManager.Instance.RestoreInput(prevInputState);

        talkDialogueUI.gameObject.SetActive(false);

        nowPlayingDialogue = null;
        OnDialogueEndAction?.Invoke();
    }


    protected override void OnSceneUnloaded(Scene scene)
    {
    }
}