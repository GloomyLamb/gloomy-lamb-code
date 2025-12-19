using UnityEngine.Events;

public class InteractableTestObject : BaseInteractableObject
{
    
    private UnityEvent action; 
    public override void Interact()
    {
        action?.Invoke();
        Logger.Log("오브젝트 상호작용");
    }
}
