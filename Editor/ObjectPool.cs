using System.Collections.Generic;
using UnityEngine;


namespace AKRN_Utilities
{
    public class ObjectPool
    {
        private readonly Queue<GameObject> pool = new Queue<GameObject>();
        private readonly GameObject prefab;

        public ObjectPool(GameObject prefab, int initialSize)
        {
            this.prefab = prefab;
            LoadInitialObjects(initialSize);
        }

        private void LoadInitialObjects(int initialSize)
        {
            for (int i = 0; i < initialSize; i++)
            {
                GameObject obj = LoadPrefab();
                if (obj != null)
                {
                    obj.SetActive(false);
                    pool.Enqueue(obj);
                }
            }
        }

        private GameObject LoadPrefab()
        {
            return GameObject.Instantiate(prefab);
        }

        public GameObject Get()
        {
            if (pool.Count == 0)
            {
                return LoadPrefab();
            }

            GameObject pooledObj = pool.Dequeue();
            pooledObj.SetActive(true);
            return pooledObj;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}


public class ObjectPoolFromResorce
{
    private readonly Queue<GameObject> pool = new Queue<GameObject>();
    private readonly string resourcePath;

    public ObjectPoolFromResorce(string resourcePath, int initialSize)
    {
        this.resourcePath = resourcePath;
        LoadInitialObjects(initialSize);
    }

    private void LoadInitialObjects(int initialSize)
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject obj = LoadPrefab();
            if (obj != null)
            {
                obj.SetActive(false);
                pool.Enqueue(obj);
            }
        }
    }

    private GameObject LoadPrefab()
    {
        GameObject prefab = Resources.Load<GameObject>(resourcePath);
        if (prefab == null)
        {
            Debug.LogError($"Prefab not found at path: {resourcePath}");
            return null;
        }
        return GameObject.Instantiate(prefab);
    }

    public GameObject Get()
    {
        if (pool.Count == 0)
        {
            return LoadPrefab();
        }

        GameObject pooledObj = pool.Dequeue();
        pooledObj.SetActive(true);
        return pooledObj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}

