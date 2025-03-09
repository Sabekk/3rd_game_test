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
        public PauseInputs PauseInputs { get; private set; }
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
            PauseInputs = new(Input);
        }

        private void Start()
        {
            AttachEvents();
            RefreshInputs();
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
            if (GameStateManager.Instance)
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

        private void RefreshInputs()
        {
            if (GameStateManager.Instance)
                switch (GameStateManager.Instance.CurrentGameState)
                {
                    case GameStateType.PLAYING:
                        CharacterInputs.Enable();
                        UiInputs.Enable();
                        PauseInputs.Disable();
                        break;
                    case GameStateType.WINDOW_VIEW:
                        CharacterInputs.Disable();
                        UiInputs.Enable();
                        PauseInputs.Disable();
                        break;
                    case GameStateType.PAUSE:
                        CharacterInputs.Disable();
                        UiInputs.Disable();
                        PauseInputs.Enable();
                        break;
                    default:
                        CharacterInputs.Enable();
                        UiInputs.Enable();
                        PauseInputs.Disable();
                        break;
                }
        }

        #region HANDLERS

        private void HandleCurrentGameStated()
        {
            RefreshInputs();
        }

        #endregion

        #endregion
    }
}
