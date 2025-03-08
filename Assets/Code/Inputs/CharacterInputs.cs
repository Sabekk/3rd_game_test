using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Inputs
{
    public class CharacterInputs : InputBinds.ICharacterActions
    {
        #region ACTIONS

        public event Action<Vector2> OnMoveInDirection;
        public event Action<Vector2> OnLookInDirection;
        public event Action OnJumping;
        public event Action OnAttackTrigger;

        #endregion

        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        public CharacterInputs() { }
        public CharacterInputs(InputBinds binds)
        {
            binds.Character.SetCallbacks(this);
        }

        #endregion

        #region METHODS

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnJumping?.Invoke();
        }

        public void OnLookAround(InputAction.CallbackContext context)
        {
            if (!context.started)
                OnLookInDirection?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            if (!context.started)
                OnMoveInDirection?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnAttackTrigger?.Invoke();
        }

        #endregion
    }
}