using ObjectPooling;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Gameplay.Cameras
{
    public class CamerasManager : MonoSingleton<CamerasManager>
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Character")] private FollowingCamera personCameraInGame;
        [SerializeField, FoldoutGroup("Settings"), ValueDropdown(ObjectPoolDatabase.GET_POOL_CAMERAS_METHOD)] private int characterCameraId;

        #endregion

        #region PROPERTIES

        public Camera MainCamera => Camera.main;
        public FollowingCamera PersonCameraInGame
        {
            get
            {
                if (personCameraInGame == null)
                    personCameraInGame = TryGetCamera(characterCameraId);
                return personCameraInGame;
            }
        }

        #endregion

        #region UNITY_METHODS

        #endregion

        #region METHODS

        private FollowingCamera TryGetCamera(int cameraId)
        {
            return ObjectPool.Instance.GetFromPool(cameraId).GetComponent<FollowingCamera>();
        }

        #endregion
    }
}
