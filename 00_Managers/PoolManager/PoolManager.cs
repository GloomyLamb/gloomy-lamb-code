using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PoolManager : SceneSingletonManager<PoolManager>
{
    [SerializeField] List<BasePool> poolsOrigin;
    Dictionary<PoolType, BasePool> poolOriginDic;
    
    Dictionary<PoolType, BasePool> nowPoolDic;

    protected override void Init()
    {
        poolOriginDic = ((PoolType[])Enum.GetValues(typeof(PoolType))).ToDictionary(part => part,
            part => (BasePool)null);

        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }


    public void UsePool(PoolType poolType)
    {
        foreach (var pool in poolsOrigin)
        {
            // if (Enum.TryParse(pool.gameObject.name, ignoreCase: true, out PoolType poolType))
            // {
            //     pool.Init();
            //     poolDic[poolType] = pool;
            // }
        }
    }

    public GameObject Spawn(PoolType poolType, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (poolOriginDic.ContainsKey(poolType))
        {
            GameObject newGameObject = poolOriginDic[poolType].GetGameObject();
            
            newGameObject.transform.position = position;
            newGameObject.transform.rotation = rotation;
            
            
            if (parent != null)
            {
                newGameObject.transform.SetParent(parent);
            }
            
            return newGameObject;
        }

        return null;
    }

    public GameObject Spawn(PoolType poolType)
    {
        if (poolOriginDic.ContainsKey(poolType))
        {
            GameObject newGameObject = poolOriginDic[poolType].GetGameObject();
            return newGameObject;
        }
        return null;
    }

    public void DeactivateAllPoolObjects(PoolType poolType)
    {
        if (poolOriginDic.ContainsKey(poolType))
        {
            poolOriginDic[poolType].DeactivateAllPoolObjects();
        }
    }

    void OnSceneUnloaded(Scene scene)
    {
        nowPoolDic.Clear();
    }
}
