using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public static PoolManager Instance = null;

    private Dictionary<string, Pool<PoolableMono>> pools = new Dictionary<string, Pool<PoolableMono>>();

    public void CreatePool(PoolableMono prefab, Transform parent)
    {
        if(pools.ContainsKey(prefab.name)) 
        { 
            Debug.LogWarning($"current name of pool already existed on pools : {prefab.name}"); 
            return;
        }

        Pool<PoolableMono> pool = new Pool<PoolableMono>(prefab, parent);
        pools.Add(prefab.name, pool);
    }

    public void Push(PoolableMono obj)
    {
        if(pools.ContainsKey(obj.name) == false) 
        { 
            Debug.LogWarning($"current name of pool doesn't existed on pools : {obj.name}"); 
            return;
        }

        pools[obj.name].Push(obj);
    }

    public PoolableMono Pop(string prefabName)
    {
        if(pools.ContainsKey(prefabName) == false) 
        { 
            Debug.LogWarning($"current name of pool doesn't existed on pools : {prefabName}, returning null"); 
            return null;
        }

        PoolableMono obj = pools[prefabName].Pop();
        obj.Reset();

        return obj;
    }
}
