using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkDialogueUI : DialogueBaseUI
{
    [Header("Name Panel")] [SerializeField]
    private GameObject namePanel;

    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Character Panel Pivot")] [SerializeField]
    private Transform leftPivot;

    [SerializeField] private Transform rightPivot;

    [Header("Characters")] 
    [SerializeField] private List<DialoguePortraitPanel> portraitPanelOrigins;
    Dictionary<string, DialoguePortraitPanel> portraitPanels = new Dictionary<string, DialoguePortraitPanel>();
    
    DialoguePortraitPanel currentPortraitPanel;
    
    public override void Init()
    {
        base.Init();
        // todo : 캐릭터를 생성해야해
    }

    protected override void NextDialogueInteranl(DialogueData dialogueData)
    {
        if (string.IsNullOrEmpty(nameText.text) == false)
        {
            namePanel.SetActive(true);
            nameText.text = nameText.text;
        }
        else
        {
            namePanel.SetActive(false);
        }

        if (string.IsNullOrEmpty(nameText.text) == false)
        {
            if (currentPortraitPanel != null)
            {
                currentPortraitPanel.gameObject.SetActive(false);
            }
            else
            {
                // currentPortraitPanel.gameObject.SetActive(true);
                // currentPortraitPanel.SetEmoption(dialogueData.Emotion);
            }
        }
    }

    // protected override void StartDialogueInternal(List<string> dialogueString)
    // {
    //     
    // }
}