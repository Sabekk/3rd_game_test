using Cinemachine;
using ObjectPooling;
using System;
using UnityEngine;

namespace Gameplay.Cameras
{
    [Serializable]
    public class FollowingCamera : MonoBehaviour, IPoolable
    {
        #region VARIABLES

        [SerializeField] private CinemachineFreeLook cvCamera;

        #endregion

        #region PROPERTIES
        public CinemachineFreeLook CvCamera => cvCamera;
        public Transform MainCameraTransform => CamerasManager.Instance != null ? CamerasManager.Instance.MainCamera.transform : null;
        public PoolObject Poolable { get; set; }

        #endregion

        #region UNITY_METHODS

        #endregion

        #region METHODS
        public void AssignPoolable(PoolObject poolable)
        {
            Poolable = poolable;
        }


        #endregion
    }
}