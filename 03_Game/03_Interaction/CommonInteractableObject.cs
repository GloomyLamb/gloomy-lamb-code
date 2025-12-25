using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommonInteractableObject : BaseInteractableObject
{
    [SerializeField] private UnityEvent _interactAction;

    public override void Interact()
    {
        _interactAction?.Invoke();
    }
}
