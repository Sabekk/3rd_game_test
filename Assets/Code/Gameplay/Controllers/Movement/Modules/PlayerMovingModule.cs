using Gameplay.Controller.Module;
using Gameplay.Inputs;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Movement
{
    public class PlayerMovingModule : CharacterMovingModule
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        protected override void AttachEvents()
        {
            base.AttachEvents();
            if (InputManager.Instance)
            {
                InputManager.Instance.CharacterInputs.OnMoveInDirection += MoveInDirection;
            }
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            if (InputManager.Instance)
            {
                InputManager.Instance.CharacterInputs.OnMoveInDirection -= MoveInDirection;
            }
        }

        #endregion
    }
}