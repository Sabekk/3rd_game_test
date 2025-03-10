using Gameplay.Cameras;
using Gameplay.Inputs;
using UnityEngine;

namespace Gameplay.Character.Movement
{
    public class PlayerMovingModule : CharacterMovingModule
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        protected Player Player => Character as Player;
        public FollowingCamera CharacterCamera => Player.PlayerMovementController.CameraModule.CharacterCamera;

        #endregion

        #region METHODS

        public override void AttachEvents()
        {
            base.AttachEvents();
            if (InputManager.Instance)
            {
                InputManager.Instance.CharacterInputs.OnMoveInDirection += HandleMoveInDirection;
                InputManager.Instance.CharacterInputs.OnLookInDirection += HandleLookInDirection;
            }
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            if (InputManager.Instance)
            {
                InputManager.Instance.CharacterInputs.OnMoveInDirection -= HandleMoveInDirection;
                InputManager.Instance.CharacterInputs.OnLookInDirection -= HandleLookInDirection;
            }
        }

        protected override void RotateCharacterByLookDirection()
        {
            if (CharacterCamera == null)
                return;

            if (CharacterCamera.MainCameraTransform != null && IsMoving)
            {
                Vector3 forward = CharacterCamera.MainCameraTransform.forward;
                forward.y = 0;
                if (forward.sqrMagnitude > 0.01f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(forward);
                    CharacterTransform.rotation = Quaternion.Slerp(CharacterTransform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                }
            }
        }

        #endregion
    }
}