using Gameplay.Cameras;
using ObjectPooling;
using System;
using UnityEngine;

namespace UI.HUD
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadingHUDElement : MonoBehaviour, IPoolable
    {
        #region ACTIONS

        public event Action OnDisposed;

        #endregion

        #region VARIABLES

        [SerializeField] protected float liveTime = 10;
        [SerializeField] float fadeTime = 3;
        [SerializeField] private CanvasGroup canvas;

        protected float timer;
        protected Transform followingTransform;

        #endregion

        #region PROPERTIES

        public PoolObject Poolable { get; set; }
        private float PercentageFade => timer / fadeTime;

        #endregion

        #region UNITY_METHODS

        private void Awake()
        {
            canvas = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            OnUpdate();
        }

        #endregion

        #region METHODS

        public virtual void Initiliaze(Transform followingTransform = null)
        {
            transform.localScale = Vector3.one * 0.01f;
            timer = liveTime;
            canvas.alpha = 1;

            this.followingTransform = followingTransform;

            if (followingTransform != null)
            {
                transform.SetParent(followingTransform);
                transform.localPosition = Vector3.zero;
            }
        }

        public virtual void OnUpdate()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                Dispose();
            else
            {
                if (timer <= fadeTime)
                    canvas.alpha = PercentageFade;

                transform.LookAt(CamerasManager.Instance.MainCamera.transform);
            }
        }

        public void Dispose()
        {
            ObjectPool.Instance.ReturnToPool(this);
            OnDisposed?.Invoke();
        }

        public void AssignPoolable(PoolObject poolable)
        {
            Poolable = poolable;
        }

        #endregion
    }
}

