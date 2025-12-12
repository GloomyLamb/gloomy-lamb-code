using System;
using UnityEngine;

public class PoolObject:MonoBehaviour
{
    public event Action<PoolObject> OnEnableAction;
    public event Action<PoolObject> OnDisableAction;


    private void OnEnable()
    {
        OnEnableAction?.Invoke(this);
    }

    private void OnDisable()
    {
        OnDisableAction?.Invoke(this);
    }
}
