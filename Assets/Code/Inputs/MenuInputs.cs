using System;
using UnityEngine.InputSystem;

namespace Gameplay.Inputs
{
    public class MenuInputs : InputsBase, InputBinds.IMenuActions
    {
        #region ACTION

        #endregion

        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        public MenuInputs(InputBinds binds) : base(binds)
        {
            Binds.Menu.SetCallbacks(this);
        }

        #endregion

        #region METHODS

        public override void Enable()
        {
            Binds.Menu.Enable();
        }

        public override void Disable()
        {
            Binds.Menu.Disable();
        }

        #endregion
    }
}