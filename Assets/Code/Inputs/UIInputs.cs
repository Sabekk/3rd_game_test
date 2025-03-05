using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Inputs
{
    public class UIInputs : InputBinds.IUIActions
    {
        #region ACTION

        public event Action OnTogglePause;
        public event Action OnToggleInventory;

        #endregion

        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        public UIInputs() { }
        public UIInputs(InputBinds binds)
        {
            binds.UI.SetCallbacks(this);
        }

        #endregion

        #region METHODS

        public void OnInventory(InputAction.CallbackContext context)
        {
            if (context.started)
                OnToggleInventory?.Invoke();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.started)
                OnTogglePause?.Invoke();
        }


        #endregion
    }
}