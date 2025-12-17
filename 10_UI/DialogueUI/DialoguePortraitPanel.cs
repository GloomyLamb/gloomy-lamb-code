using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePortraitPanel : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] private Image characterImg;
    [SerializeField] private Image characterEmotionImg;
    
    [Header("Sprites")]
    [SerializeField] Sprite defaultSpr;
    
    
    public void SetEmoption(DialogueEmotionType emotion)
    {
        switch (emotion)
        {
            case DialogueEmotionType.Default:
                break;
            case DialogueEmotionType.Smile:
                break;
            case DialogueEmotionType.Sad:
                break;
            case DialogueEmotionType.Angry:
                break;
            case DialogueEmotionType.Shock:
                break;
            case DialogueEmotionType.Confusion:
                break;
        }
    }
    
    
    

}
