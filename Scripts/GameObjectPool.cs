using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool
{
    private static GameObjectPool _Instance;
    public static GameObjectPool Instance
    {
        get
        {
            if (_Instance == null)
                _Instance = new GameObjectPool();
            return _Instance;
        }
    }
    public Dictionary<string, Transform> poolDict = new Dictionary<string, Transform>();
    public Transform getPool(string poolName)
    {
        if (poolDict.ContainsKey(poolName))
            return poolDict[poolName];
        // Create if there isnt the transform in the pool
        Transform poolObj = new GameObject(poolName + "_Pool").transform;
        poolObj.gameObject.SetActive(false);
        poolDict.Add(poolName, poolObj);
        return poolObj;
    }
}
