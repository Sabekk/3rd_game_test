using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Inputs
{
    public class UIInputs : InputsBase, InputBinds.IUIActions
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

        public UIInputs(InputBinds binds) : base(binds)
        {
            Binds.UI.SetCallbacks(this);
        }

        #endregion

        #region METHODS

        public override void Enable()
        {
            Binds.UI.Enable();
        }

        public override void Disable()
        {
            Binds.UI.Disable();
        }

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