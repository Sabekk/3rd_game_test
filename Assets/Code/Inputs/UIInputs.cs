using System;
using UnityEngine.InputSystem;

namespace Gameplay.Inputs
{
    public class UIInputs : InputsBase, InputBinds.IUIActions
    {
        #region ACTION

        public event Action OnForceCloseLast;
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

        public void OnCloseLast(InputAction.CallbackContext context)
        {
            if (context.started)
                OnForceCloseLast?.Invoke();
        }

        #endregion
    }
}