using Gameplay.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Movement
{
    public class PlayerMovementController : CharacterControllerBase
    {
        #region VARIABLES

        [SerializeField] private PlayerAnimatorStateModule animatorModule;
        [SerializeField] private PlayerMovingModule movingModule;
        [SerializeField] private CameraMovementModule cameraModule;

        #endregion

        #region PROPERTIES

        public PlayerAnimatorStateModule AnimatorModule => animatorModule;
        public PlayerMovingModule MovingModule => movingModule;
        public CameraMovementModule CameraModule => cameraModule;

        #endregion

        #region METHODS

        public override void SetModules()
        {
            base.SetModules();

            modules.Add(animatorModule = new());
            modules.Add(movingModule = new());
            modules.Add(cameraModule = new());
        }


        #endregion
    }
}
