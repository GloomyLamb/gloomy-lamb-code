using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : SceneSingletonManager<PoolManager>
{
    [SerializeField] List<BasePool> pools;
    Dictionary<PoolType, BasePool> poolDic;

    protected override void Init()
    {
        poolDic = ((PoolType[])Enum.GetValues(typeof(PoolType))).ToDictionary(part => part,
            part => (BasePool)null);

        foreach (var pool in pools)
        {
            if (Enum.TryParse(pool.gameObject.name, ignoreCase: true, out PoolType poolType))
            {
                pool.Init();
                poolDic[poolType] = pool;
            }
        }
    }

    public GameObject Spawn(PoolType poolType, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (poolDic.ContainsKey(poolType))
        {
            GameObject newGameObject = poolDic[poolType].GetGameObject();
            
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
        if (poolDic.ContainsKey(poolType))
        {
            GameObject newGameObject = poolDic[poolType].GetGameObject();
            return newGameObject;
        }
        return null;
    }

    public void DeactivateAllPoolObjects(PoolType poolType)
    {
        if (poolDic.ContainsKey(poolType))
        {
            poolDic[poolType].DeactivateAllPoolObjects();
        }
    }
}
