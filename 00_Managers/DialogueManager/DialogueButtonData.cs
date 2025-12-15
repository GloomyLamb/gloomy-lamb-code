using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueButtonData
{
    public string ButtonDescriptions => buttonDescriptions;
    [SerializeField] private string buttonDescriptions;
}

[Serializable]
public class NextDialogueButtonData : DialogueButtonData
{
    private DialogueAsset NextDialogueAsset => nextDialogueAssets;
    [SerializeField] private DialogueAsset nextDialogueAssets;
}