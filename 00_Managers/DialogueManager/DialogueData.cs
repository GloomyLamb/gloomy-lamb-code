using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueData
{
    [SerializeField] private string name;
    [SerializeField] private string dialogue;
    [SerializeField] private string sprName;
    [SerializeField] private DialogueEmotionType emotion;
    [SerializeField] List<DialogueButtonData> buttons;
}