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
    protected virtual void Init()
    {
        
    }

    /// <summary>
    /// 외부 초기화
    /// </summary>
    public virtual void Setup()
    {
        
    }
    
    // 현재 불필요
    // void RegisterUI()
    // {
    // }
    //
    // private void OnDestroy()
    // {
    //     UnRegisterUI();
    // }
    //
    // void UnRegisterUI()
    // {
    //     
    // }

}
