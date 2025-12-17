using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkDialogueUI : DialogueBaseUI
{
    [Header("Name Panel")] 
    [SerializeField] private GameObject namePanel;

    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Character Panel Pivot")] 
    [SerializeField] private Transform leftPivot;
    [SerializeField] private Transform rightPivot;
    
    [Header("Buttons")]
    [SerializeField] private Button nextButton;
    [SerializeField] private TextMeshProUGUI buttonText;
    private const string NextString = "다음";
    private const string CloseString = "닫기";
    private const string SelectString = "선택";
    

    // 피드백 
    // 이후 어드레서블을 사용하게되면 이 부분은 테이블화 된거에서 빼오면 됨.
    // portrait 프리팹화해서 쓴거 굿
    [Header("Characters")] 
    [SerializeField] private List<DialoguePortraitPanel> portraitPanelOrigins;
    Dictionary<PortraitCharacter, DialoguePortraitPanel> portraitPanels = new Dictionary<PortraitCharacter, DialoguePortraitPanel>();
    DialoguePortraitPanel currentPortraitPanel;
    
    

    public override void Setup()
    {
        nextButton.onClick.AddListener(OnNextDialogueButton);
        
        for (int i = 0; i < portraitPanelOrigins.Count; i++)
        {
            if (portraitPanelOrigins[i] != null)
            {
                string[] portraitNameSplit = portraitPanelOrigins[i].name.Split("_");
                if (portraitNameSplit.Length > 1)
                {
                    string portraitName = portraitNameSplit[1];
                    DialoguePortraitPanel newPortrait = null;

                    if (portraitName == PortraitCharacter.Dusky.ToString())
                    {
                        newPortrait = Instantiate(portraitPanelOrigins[i], leftPivot);
                    }
                    else
                    {
                        newPortrait = Instantiate(portraitPanelOrigins[i], rightPivot);
                    }

                    if (newPortrait != null)
                    {
                        newPortrait.gameObject.SetActive(false);
                        newPortrait.name = $"portrait_{portraitName}";
                        if (PortraitCharacter.TryParse(portraitName, out PortraitCharacter portraitCharacter))
                        {
                            if (portraitPanels.ContainsKey(portraitCharacter) == false)
                            {
                                portraitPanels.Add(portraitCharacter, newPortrait);
                            }
                        }
                    }
                }
            }
        }
    }


    protected override void NextDialogueInteranl(DialogueData dialogueData, bool lastDialogue = false)
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

        if (dialogueData.PortraitCharacter == PortraitCharacter.None)
        {
            currentPortraitPanel?.gameObject.SetActive(false);
        }
        else
        {
            currentPortraitPanel?.gameObject.SetActive(false);
            if (portraitPanels.TryGetValue(dialogueData.PortraitCharacter, out DialoguePortraitPanel portraitPanel))
            {
                currentPortraitPanel = portraitPanel;
                currentPortraitPanel?.gameObject.SetActive(true);
                currentPortraitPanel?.SetEmotion(dialogueData.Emotion);
            }
        }

        if (lastDialogue)
        {
            if (dialogueData.Buttons.Count > 0)
            {
                buttonText.text = SelectString;
            }
            else
            {
                buttonText.text = CloseString;
            }
        }
        else
        {
            buttonText.text = NextString;
        }
    }

}