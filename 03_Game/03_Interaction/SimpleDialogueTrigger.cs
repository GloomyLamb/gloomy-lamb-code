using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDialogueTrigger : BaseInteractableObject
{
    [SerializeField] private DialogueAsset _dialogue;
    public override void Interact()
    {
        DialogueManager.Instance?.StartDialogue(_dialogue);
    }
}
