using UnityEngine;

public abstract class ShadowController : MonoBehaviour
{
    // todo: 각종 스텟
    [field: Header("Target")]
    [field: SerializeField] public Transform Target;

    protected virtual void Start()
    {
        if (Target == null)
        {
            Target = FindObjectOfType<Player>().transform;
        }
    }
}
