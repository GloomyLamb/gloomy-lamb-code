using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DialogueAsset", menuName = "SO/Dialogue Asset")]
public class DialogueAsset : ScriptableObject
{
    [SerializeField] private TextAsset dialogueOrigin;
    [SerializeField] private List<DialogueData> dialogueList;

    
    public void Parse()
    {
        string text = dialogueOrigin.text;
        List<Dictionary<string, object>> parseText = CSVReader.Read(dialogueOrigin);

        if (parseText == null || parseText.Count == 0) return;
        
        dialogueList = new List<DialogueData>();

        for (int i = 0; i < parseText.Count; i++)
        {
            Debug.Log("?");
            string name = parseText[i].ContainsKey("Name") ? parseText[i]["Name"].ToString() : "";
            string dialogue  = parseText[i].ContainsKey("Dialogue") ? parseText[i]["Dialogue"].ToString() : "";
            string sprite = parseText[i].ContainsKey("Sprite") ? parseText[i]["Sprite"].ToString() : "";
            
            string emotionString =  parseText[i].ContainsKey("Emotion") ? parseText[i]["Emotion"].ToString() : "";
            DialogueEmotionType emotionType = DialogueEmotionType.Default;
            DialogueEmotionType.TryParse(emotionString, out emotionType);
            
            string buttonsText = parseText[i].ContainsKey("Buttons") ? parseText[i]["Buttons"].ToString() : "";
            List<DialogueButtonData> newButtons = new List<DialogueButtonData>();
            if (string.IsNullOrEmpty(buttonsText) == false)
            {
                string[] buttonStrings =buttonsText.Split("|",StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < buttonStrings.Length; j++)
                {
                    DialogueButtonData newButtonData = new DialogueButtonData(DialogueButtonType.NextDialogue, buttonStrings[j]);
                    newButtons.Add(newButtonData);
                }    
            }
            
            DialogueData newDialogue = new DialogueData(name, dialogue, sprite, emotionType, newButtons);
            
            //DialogueEmotionType.TryParse
            //DialogueData newDialogue = new DialogueData(); 
            dialogueList.Add(newDialogue);
        }
    }
    
    public void RemoveDialogueData()
    {
        if(dialogueList != null)
            dialogueList.Clear();
    }

#if UNITY_EDITOR
    // private void OnValidate()
    // {
    //     if (dialogueOrigin == null) return;
    //     Parse();
    // }
#endif
}