using Gameplay.GameStates;
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

        private void Start()
        {
            AttachEvents();
        }

        private void OnDestroy()
        {
            DetachEvents();
        }

        private void OnEnable() => Input.Enable();

        private void OnDisable() => Input.Disable();

        #endregion

        #region METHODS

        private void AttachEvents()
        {
            if(GameStateManager.Instance)
            {
                GameStateManager.Instance.OnCurrentGameStated += HandleCurrentGameStated;
            }
        }

        private void DetachEvents()
        {
            if (GameStateManager.Instance)
            {
                GameStateManager.Instance.OnCurrentGameStated -= HandleCurrentGameStated;
            }
        }


        #region HANDLERS

        private void HandleCurrentGameStated()
        {
            if(GameStateManager.Instance)
                switch (GameStateManager.Instance.CurrentGameState)
                {
                    case GameStateType.GAMEPLAY:
                        CharacterInputs.Enable();
                        UiInputs.Enable();
                        break;
                    case GameStateType.WINDOW_VIEW:
                        CharacterInputs.Disable();
                        UiInputs.Enable();
                        break;
                    case GameStateType.PAUSE:
                        CharacterInputs.Disable();
                        UiInputs.Disable();
                        break;
                    default:
                        CharacterInputs.Enable();
                        UiInputs.Enable();
                        break;
                }
        }

        #endregion

        #endregion
    }
}
