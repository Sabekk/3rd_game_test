using Gameplay.Timing;
using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UI.Window;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoSingleton<UIManager>
    {
        #region ACTION

        public event Action<UIWindowBase> OnWindowOpened;
        public event Action<UIWindowBase> OnWindowClosed;

        #endregion

        #region VARIABLES

        [SerializeField] private Canvas mainCanvas;
        [SerializeField, ValueDropdown(ObjectPoolDatabase.GET_POOL_CATEGORIES_METHOD)] private int defaultUIPoolCategory;

        private List<UIWindowBase> openedWindows;
        private UIManagerBindsListener bindsListener;

        #endregion

        #region PROPERTIES
        public int DefaultUIPoolCategory => defaultUIPoolCategory;
        public bool AnyWindowIsOpened => openedWindows.Count > 0;

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
        public T OpenWindow<T>(int poolCategory, int poolWindowId) where T : UIWindowBase
        {
            PoolObject poolObject = GetPoolWindow(poolCategory, poolWindowId);

            if (poolObject == null)
                return null;

            return OpenWindow<T>(poolObject);
        }

        public T OpenWindow<T>(int poolCategory, string poolWindowName) where T : UIWindowBase
        {
            PoolObject poolObject = GetPoolWindow(poolCategory, poolWindowName);

            if (poolObject == null)
                return null;

            return OpenWindow<T>(poolObject);
        }

        public T OpenWindow<T>(PoolObject poolObject) where T : UIWindowBase
        {
            T window = poolObject.GetComponent<T>();

            if (window == null)
            {
                Debug.LogError($"[{GetType().Name}] Missing window type from pool {poolObject.Category} - {poolObject.Name} - {typeof(T).Name}");
                return null;
            }

            window.Initialize();
            window.OnCloseWindow += CloseWindow<T>;

            //If is opened but for firstly
            if (openedWindows.Contains(window) && openedWindows.GetLastElement() != window)
            {
                openedWindows.Remove(window);
                openedWindows.SetActiveOptimizeLastElement(false);

                openedWindows.Add(window);
            }
            else
            {
                openedWindows.SetActiveOptimizeLastElement(false);
                openedWindows.Add(window);
                openedWindows.SetActiveOptimizeLastElement(true);

                if (TimeManager.Instance)
                    TimeManager.Instance.TryToggleTime(false);

                OnWindowOpened?.Invoke(window);
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

                    if (openedWindows.Count == 0)
                    {
                        if (TimeManager.Instance)
                            TimeManager.Instance.TryToggleTime(true);
                    }
                }
                else
                {
                    openedWindows.Remove(window);
                }

                window.OnCloseWindow -= CloseWindow<T>;
                OnWindowClosed?.Invoke(window);

                window.CleanUp();
                ObjectPool.Instance.ReturnToPool(window);
            }
        }

        public void TryCloseLastOpenedWindow()
        {
            UIWindowBase window = openedWindows.GetLastElement();
            if (window != null)
                window.CloseFromUI();
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
