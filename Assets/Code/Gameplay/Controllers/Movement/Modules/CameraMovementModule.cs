using Gameplay.Cameras;
using Gameplay.Controller.Module;

namespace Gameplay.Character.Movement
{
    public class CameraMovementModule : ControllerModuleBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES
        public FollowingCamera CharacterCamera => CamerasManager.Instance != null ? CamerasManager.Instance.PersonCameraInGame : null;

        #endregion

        #region METHODS

        protected override void AttachEvents()
        {
            base.AttachEvents();
            Character.OnCharacterInGameCreated+= HandleCharacterInGameCreated;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Character.OnCharacterInGameCreated -= HandleCharacterInGameCreated;
        }


        #region HANDLERS

        private void HandleCharacterInGameCreated()
        {
            CharacterCamera.CvCamera.Follow = Character.CharacterInGame.transform;
            CharacterCamera.CvCamera.LookAt = Character.CharacterInGame.transform;
        }

        #endregion

        #endregion
    }
}