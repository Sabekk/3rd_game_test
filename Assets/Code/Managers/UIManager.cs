using ObjectPooling;
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
        [SerializeField] private string defaultUIWindowPoolCategory = "UIWindow";

        private List<UIWindowBase> openedWindows;

        #endregion

        #region PROPERTIES

        #endregion

        #region UNITY_METHODS

        protected override void Awake()
        {
            base.Awake();
            openedWindows = new();
        }

        #endregion

        #region METHODS

        public T OpenWindow<T>(string poolCategory, string poolWindowId, bool registerAsOpened = true) where T : UIWindowBase
        {
            PoolObject poolObject = ObjectPool.Instance.GetFromPool(poolWindowId, poolCategory);

            if (poolObject == null)
            {
                Debug.LogError($"[{GetType().Name}] Missing window in pool {poolCategory} - {poolWindowId}");
                return null;
            }

            T window = poolObject.GetComponent<T>();

            if (window == null)
            {
                Debug.LogError($"[{GetType().Name}] Missing window type from pool {poolCategory} - {poolWindowId} - {typeof(T).Name}");
                return null;
            }

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

        public T OpenWindow<T>(string poolWindowId) where T : UIWindowBase
        {
            return OpenWindow<T>(defaultUIWindowPoolCategory, poolWindowId);
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

        #endregion
    }
}
