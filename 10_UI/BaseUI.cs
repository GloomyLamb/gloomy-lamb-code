using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }
    public virtual void Init()
    {
        
    }


    void RegisterUI()
    {
        
    }


    private void OnDestroy()
    {
        UnRegisterUI();
    }
    
    void UnRegisterUI()
    {
        
    }


}
