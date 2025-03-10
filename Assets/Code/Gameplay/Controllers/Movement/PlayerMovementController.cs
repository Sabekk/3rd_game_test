using Gameplay.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Movement
{
    public class PlayerMovementController : CharacterMovementController
    {
        #region VARIABLES

        [SerializeField] private CameraMovementModule cameraModule;

        #endregion

        #region PROPERTIES

        public CameraMovementModule CameraModule => cameraModule;

        #endregion

        #region METHODS

        public override void CreateModules()
        {
            animatorModule = new PlayerAnimatorStateModule();
            movingModule = new PlayerMovingModule();
            cameraModule = new CameraMovementModule();
        }

        public override void SetModules()
        {
            base.SetModules();
            modules.Add(cameraModule);
        }


        #endregion
    }
}
