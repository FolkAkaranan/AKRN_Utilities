using System;
using System.Collections.Generic;
using UnityEngine;

namespace AKRN_Utilities
{
    public class ObjectPool<T>
    {
        private readonly Queue<T> pool = new();
        private readonly Func<T> createFunc;

        public ObjectPool(Func<T> createFunc, int initialSize)
        {
            this.createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));
            LoadInitialObjects(initialSize);
        }

        private void LoadInitialObjects(int initialSize)
        {
            for (int i = 0; i < initialSize; i++)
            {
                pool.Enqueue(createFunc());
            }
        }

        public T Get()
        {
            return pool.Count > 0 ? pool.Dequeue() : createFunc();
        }

        public void ReturnToPool(T obj)
        {
            pool.Enqueue(obj);
        }
    }

    public class GameObjectPool : ObjectPool<GameObject>
    {
        public GameObjectPool(GameObject prefab, int initialSize)
            : base(() => GameObject.Instantiate(prefab), initialSize)
        {
        }
    }

    public class GameObjectPoolFromResource : ObjectPool<GameObject>
    {
        public GameObjectPoolFromResource(string resourcePath, int initialSize)
            : base(() =>
            {
                GameObject prefab = Resources.Load<GameObject>(resourcePath);
                if (prefab == null)
                {
                    Debug.LogError($"Prefab not found at path: {resourcePath}");
                    return null;
                }
                return GameObject.Instantiate(prefab);
            }, initialSize)
        {
        }
    }
}
