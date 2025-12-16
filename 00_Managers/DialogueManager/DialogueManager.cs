using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DialogueManager : GlobalSingletonManager<DialogueManager>
{
    public event Action OnDialogueStartAction;
    public event Action OnDialogueEndAction;
    
    
    protected override void Init()
    {
        
        
    }


    public void StartDialogue(DialogueAsset dialogueAsset)
    {
        OnDialogueStartAction?.Invoke();
    }

    
    public void EndDialogue()
    {
        OnDialogueEndAction?.Invoke();
    }
    

    protected override void OnSceneUnloaded(Scene scene)
    {
        
    }


}
