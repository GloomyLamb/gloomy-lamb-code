using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class DialogueData
{
    public DialogueType Type => type;
    public string Name => name;
    public string Dialogue => dialogue;
    public string SprName => sprName;
    public DialogueEmotionType Emotion => emotion;
    public List<DialogueButtonData> Buttons => buttons; 
    
    [SerializeField] private DialogueType type;
    [SerializeField] private string name;
    [SerializeField] private string dialogue;
    [SerializeField] private string sprName;
    [SerializeField] private DialogueEmotionType emotion;
    [SerializeField] List<DialogueButtonData> buttons;


    public DialogueData(string _name, string _dialogue, string _sprName, DialogueEmotionType _emotion, List<DialogueButtonData> _buttons)
    {
        // type = _type;
        name = _name;
        dialogue = _dialogue;
        sprName = _sprName;
        emotion = _emotion;
        buttons = _buttons;

    }
    // todo : 대화 종료 후 이벤트 처리에 대한 게 있으면 뭔가 해줘야함..!
}