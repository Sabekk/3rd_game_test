using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UI.Window;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoSingleton<UIManager>
    {
        #region VARIABLES

        [SerializeField] private Canvas mainCanvas;
        [SerializeField, ValueDropdown(ObjectPoolDatabase.GET_POOL_CATEGORIES_METHOD)] private int defaultUIPoolCategory;

        private List<UIWindowBase> openedWindows;
        private UIManagerBindsListener bindsListener;

        #endregion

        #region PROPERTIES
        public int DefaultUIPoolCategory => defaultUIPoolCategory;

        #endregion

        #region UNITY_METHODS

        protected override void Awake()
        {
            base.Awake();
            openedWindows = new();
            bindsListener = new();
        }

        private void Start()
        {
            bindsListener.Initialize(this);
        }

        private void OnDestroy()
        {
            CleanUp();
        }

        #endregion

        #region METHODS

        public T OpenWindow<T>(int poolWindowId) where T : UIWindowBase
        {
            return OpenWindow<T>(defaultUIPoolCategory, poolWindowId);
        }

        public T OpenWindow<T>(string poolWindownName) where T : UIWindowBase
        {
            return OpenWindow<T>(defaultUIPoolCategory, poolWindownName);
        }
        public T OpenWindow<T>(int poolCategory, int poolWindowId, bool registerAsOpened = true) where T : UIWindowBase
        {
            PoolObject poolObject = GetPoolWindow(poolCategory, poolWindowId);

            if (poolObject == null)
                return null;

            return OpenWindow<T>(poolObject, registerAsOpened);
        }

        public T OpenWindow<T>(int poolCategory, string poolWindowName, bool registerAsOpened = true) where T : UIWindowBase
        {
            PoolObject poolObject = GetPoolWindow(poolCategory, poolWindowName);

            if (poolObject == null)
                return null;

            return OpenWindow<T>(poolObject, registerAsOpened);
        }

        public T OpenWindow<T>(PoolObject poolObject, bool registerAsOpened = true) where T : UIWindowBase
        {
            T window = poolObject.GetComponent<T>();
            window.Initialize();

            if (window == null)
            {
                Debug.LogError($"[{GetType().Name}] Missing window type from pool {poolObject.Category} - {poolObject.Name} - {typeof(T).Name}");
                return null;
            }

            window.OnCloseWindow += CloseWindow<T>;

            //If is opened but for firstly
            if (openedWindows.Contains(window) && openedWindows.GetLastElement() != window)
            {
                openedWindows.Remove(window);
                openedWindows.SetActiveOptimizeLastElement(false);

                if (registerAsOpened)
                    openedWindows.Add(window);
                else
                    window.gameObject.SetActiveOptimize(true);
            }
            else
            {
                if (registerAsOpened)
                {
                    openedWindows.SetActiveOptimizeLastElement(false);
                    openedWindows.Add(window);
                    openedWindows.SetActiveOptimizeLastElement(true);
                }
                else
                    window.gameObject.SetActiveOptimize(true);
            }

            window.gameObject.transform.SetParent(mainCanvas.transform, false);

            return window;
        }

        public void CloseWindow<T>() where T : UIWindowBase
        {
            Type windowType = typeof(T);
            UIWindowBase window = openedWindows.Find(x => x.GetType() == windowType);

            if (window != null)
            {
                if (openedWindows.GetLastElement() == window)
                {
                    openedWindows.SetActiveOptimizeLastElement(false);
                    openedWindows.Remove(window);
                    openedWindows.SetActiveOptimizeLastElement(true);
                }
                else
                {
                    openedWindows.Remove(window);
                }

                window.OnCloseWindow -= CloseWindow<T>;
                window.CleanUp();
                ObjectPool.Instance.ReturnToPool(window);
            }
        }


        public bool IsOpenened<T>() where T : UIWindowBase
        {
            Type windowType = typeof(T);
            UIWindowBase window = openedWindows.Find(x => x.GetType() == windowType);

            return window != null;
        }

        public void CloseAllWindow()
        {
            for (int i = openedWindows.Count - 1; i >= 0; i--)
            {
                openedWindows[i].CleanUp();
                ObjectPool.Instance.ReturnToPool(openedWindows[i]);
            }

            openedWindows.Clear();
        }

        private PoolObject GetPoolWindow(int poolCategory, int poolWindowId)
        {
            PoolObject poolObject = ObjectPool.Instance.GetFromPool(poolWindowId, poolCategory, false);

            if (poolObject == null)
            {
                Debug.LogError($"[{GetType().Name}] Missing window in pool {poolCategory} - {poolWindowId}");
                return null;
            }

            return poolObject;
        }

        private PoolObject GetPoolWindow(int poolCategory, string poolWindowName)
        {
            PoolObject poolObject = ObjectPool.Instance.GetFromPool(poolWindowName, poolCategory, false);

            if (poolObject == null)
            {
                Debug.LogError($"[{GetType().Name}] Missing window in pool {poolCategory} - {poolWindowName}");
                return null;
            }

            return poolObject;
        }

        private void CleanUp()
        {
            CloseAllWindow();
            bindsListener.CleanUp();
        }

        #endregion
    }
}
