using System;
using UnityEngine;
using System.Collections.Generic;

public class VillageSpawn : MonoBehaviour
{

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    #region Singleton
    public static VillageSpawn instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> poolObj = new Queue<GameObject>();
            for (int i = 0; i < pool.Size; i++)
            {
                GameObject obj = Instantiate(pool.PoolPrefab);
                obj.SetActive(false);
                poolObj.Enqueue(obj);
            }
            poolDictionary.Add(pool.Tag, poolObj);
        }
    }

    public GameObject SpawnItems(string Tag, Vector3 spawnPos)
    {
        if (!poolDictionary.ContainsKey(Tag))
        {
            return null;
        }

        Queue<GameObject> objectQueue = poolDictionary[Tag];

        // Get the first object in the queue (oldest object)
        GameObject newObj = objectQueue.Dequeue();

        // If the object is already active, disable it first
        if (newObj.activeSelf)
        {
            newObj.SetActive(false);
        }

        // Set the object active and move it to the spawn position
        newObj.transform.position = spawnPos;
        newObj.SetActive(true);

        // Re-enqueue the object back to the pool
        objectQueue.Enqueue(newObj);

        return newObj;
    }
}

[System.Serializable]
public class Pool
{
    public string Tag;
    public int Size;
    public GameObject PoolPrefab;
}
