using Gameplay.GameStates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay.Inputs
{
    public class PauseInputs : InputsBase, InputBinds.IPauseActions
    {
        #region ACTION

        public event Action OnTogglePause;

        #endregion

        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        public PauseInputs(InputBinds binds) : base(binds)
        {
            Binds.Pause.SetCallbacks(this);
        }

        #endregion

        #region METHODS

        public override void Enable()
        {
            Binds.Pause.Enable();
        }

        public override void Disable()
        {
            Binds.Pause.Disable();
        }

        public void OnPause(InputAction.CallbackContext context)
        {
            if (context.started)
                OnTogglePause?.Invoke();
        }

        #endregion
    }
}