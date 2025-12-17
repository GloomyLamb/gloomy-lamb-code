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

    // 피드백 
    // 이후 어드레서블을 사용하게되면 이 부분은 테이블화 된거에서 빼오면 됨.
    // portrait 프리팹화해서 쓴거 굿
    [Header("Characters")] 
    [SerializeField] private List<DialoguePortraitPanel> portraitPanelOrigins;
    Dictionary<string, DialoguePortraitPanel> portraitPanels = new Dictionary<string, DialoguePortraitPanel>();
    
    DialoguePortraitPanel currentPortraitPanel;
    
    public void InitPortrait()
    {
        
        // todo : 캐릭터를 생성해야해
    }

    protected override void NextDialogueInteranl(DialogueData dialogueData)
    {
        if (string.IsNullOrEmpty(dialogueData.Name))
        {
            namePanel.SetActive(false);
        }
        else
        {
            namePanel.SetActive(true);
            nameText.text = dialogueData.Name;
        }

        if (string.IsNullOrEmpty(dialogueData.SprName) == false)
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