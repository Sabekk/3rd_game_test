using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Inputs
{
    public class InputManager : MonoSingleton<InputManager>
    {
        #region VARIABLES

        static InputBinds _controll;

        #endregion

        #region PROPERTIES

        public UIInputs UiInputs { get; private set; }
        public CharacterInputs CharacterInputs { get; private set; }
        public static InputBinds Input
        {
            get
            {
                if (_controll == null)
                    _controll = new InputBinds();
                return _controll;
            }
        }


        #endregion

        #region UNITY_METHODS

        protected override void Awake()
        {
            base.Awake();
            UiInputs = new(Input);
            CharacterInputs = new(Input);
        }

        private void OnEnable() => Input.Enable();

        private void OnDisable() => Input.Disable();

        #endregion
    }
}
