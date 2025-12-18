using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventTrigger : MonoBehaviour
{
    public  UnityEvent OnAttackAction;

    void OnAttack()
    {
        OnAttackAction?.Invoke();
    }
    
}
