using Database;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    public class ObjectPool : MonoSingleton<ObjectPool>
    {
        #region VARIABLES

        [SerializeField] private List<PoolCategory> poolCategories;
        [SerializeField] private Transform mainPoolParent;

        #endregion

        #region PROPERTIES

        public Transform MainPoolParent
        {
            get
            {
                if (mainPoolParent == null)
                    mainPoolParent = new GameObject("Pools").transform;

                return mainPoolParent;
            }
        }

        #endregion

        #region UNITY_METHODS

        protected override void Awake()
        {
            base.Awake();
            InitializePools();
        }

        #endregion

        #region METHODS

        public PoolObject GetFromPool(string instanceName, string categoryName = "", bool activateObject = true)
        {
            foreach (var category in poolCategories)
            {
                if (!string.IsNullOrEmpty(categoryName))
                    if (category.Name != categoryName)
                        continue;

                PoolInstance instance = category.TryGetInstance(instanceName);

                if (instance != null)
                    return instance.GetFromPool(activateObject);
                else if (!string.IsNullOrEmpty(categoryName))
                {
                    Debug.LogError("Pool cannot be found");
                    return null;
                }
            }

            Debug.LogError("Pool cannot be found");
            return null;
        }

        public PoolObject GetFromPool(int instanceId, string categoryName = "", bool activateObject = true)
        {
            foreach (var category in poolCategories)
            {
                if (!string.IsNullOrEmpty(categoryName))
                    if (category.Name != categoryName)
                        continue;

                PoolInstance instance = category.TryGetInstance(instanceId);

                if (instance != null)
                    return instance.GetFromPool(activateObject);
                else if (!string.IsNullOrEmpty(categoryName))
                {
                    Debug.LogError("Pool cannot be found");
                    return null;
                }
            }

            Debug.LogError("Pool cannot be found");
            return null;
        }

        public PoolObject GetFromPool(string instanceName, int categoryId = -1, bool activateObject = true)
        {
            foreach (var category in poolCategories)
            {
                if (categoryId != -1)
                    if (!category.IdEquals(categoryId))
                        continue;

                PoolInstance instance = category.TryGetInstance(instanceName);

                if (instance != null)
                    return instance.GetFromPool(activateObject);
                else if (categoryId != -1)
                {
                    Debug.LogError("Pool cannot be found");
                    return null;
                }
            }

            Debug.LogError("Pool cannot be found");
            return null;
        }

        public PoolObject GetFromPool(int instanceId, int categoryId = -1, bool activateObject = true)
        {
            foreach (var category in poolCategories)
            {
                if (categoryId != -1)
                    if (!category.IdEquals(categoryId))
                        continue;

                PoolInstance instance = category.TryGetInstance(instanceId);

                if (instance != null)
                    return instance.GetFromPool(activateObject);
                else if (categoryId != -1)
                {
                    Debug.LogError("Pool cannot be found");
                    return null;
                }
            }

            Debug.LogError("Pool cannot be found");
            return null;
        }


        public void ReturnToPool(IPoolable poolableObject)
        {
            PoolObject poolObj = poolableObject.Poolable;

            foreach (var category in poolCategories)
            {
                if (category.Name != poolObj.Category)
                    continue;

                PoolInstance instance = category.TryGetInstance(poolObj.Name);

                if (instance != null)
                    instance.ReturnToPool(poolObj);
                else
                    Debug.LogError("Object is not from pool", poolObj.Prefab);

                break;
            }
        }

        private void InitializePools()
        {
            foreach (var poolCategoryData in MainDatabases.Instance.ObjectPoolDatabase.PoolCategories)
                poolCategories.Add(new PoolCategory(poolCategoryData));

            foreach (var poolCategory in poolCategories)
                poolCategory.Initialzie();

        }

        #endregion
    }
}