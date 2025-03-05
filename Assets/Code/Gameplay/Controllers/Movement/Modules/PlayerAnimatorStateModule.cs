using Gameplay.Controller.Module;
using Gameplay.Inputs;
using UnityEngine;

namespace Gameplay.Character.Movement
{
    public class PlayerAnimatorStateModule : CharacterAnimatorStateModule
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