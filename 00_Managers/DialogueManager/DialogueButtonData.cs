using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueButtonData
{
    public DialogueButtonType Type => type;
    [SerializeField] DialogueButtonType type;

    public string ButtonDescriptions => buttonDescriptions;
    [SerializeField] private string buttonDescriptions;

    private DialogueAsset NextDialogueAsset => nextDialogueAssets;
    [SerializeField] private DialogueAsset nextDialogueAssets;

    public DialogueButtonData(DialogueButtonType _type, string _buttonDescriptions, DialogueAsset _nextDialogueAsset = null)
    {
        type = _type;
        buttonDescriptions = _buttonDescriptions;
        nextDialogueAssets = _nextDialogueAsset;
    }
}