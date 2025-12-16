using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkDialogueUI : DialogueBaseUI
{
    [Header("Characters")]
    [SerializeField] private List<DialoguePortraitPanel> portraitPanelOrigins;
    List<DialoguePortraitPanel> portraitPanels;

    public override void ShowDialogue(DialogueData dialogueData)
    {
        
    }

    public override void PrintDialogue(string dialogueString)
    {
        
    }

}
