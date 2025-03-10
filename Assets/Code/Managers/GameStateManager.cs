using Gameplay.Scenes;
using Sirenix.OdinInspector;
using System;
using UI;
using UI.Window;
using UI.Window.Pause;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.GameStates
{
    public class GameStateManager : GameplayManager<GameStateManager>
    {
        #region ACTION

        [SerializeField] GameStateType currentGameState;
        public event Action OnCurrentGameStated;

        #endregion

        #region VARIABLES

        #endregion

        #region PROPERTIES

        public GameStateType CurrentGameState
        {
            get { return currentGameState; }
            private set { currentGameState = value; }
        }

        #endregion

        #region METHODS

        public override void LateInitialzie()
        {
            base.LateInitialzie();
            SetStartingState();
        }

        protected override void AttachEvents()
        {
            if (UIManager.Instance)
            {
                UIManager.Instance.OnWindowOpened += HandleWindowOpened;
                UIManager.Instance.OnWindowClosed += HandleWindowClosed;
            }

            SceneManager.activeSceneChanged += HandleSceneLoaded;
        }

        protected override void DetachEvents()
        {
            if (UIManager.Instance)
            {
                UIManager.Instance.OnWindowOpened -= HandleWindowOpened;
                UIManager.Instance.OnWindowClosed -= HandleWindowClosed;
            }

            SceneManager.activeSceneChanged -= HandleSceneLoaded;
        }

        private void SetStartingState()
        {
            bool setted = false;
            if (UIManager.Instance)
            {
                if (UIManager.Instance.IsOpenened<UIPauseWindow>())
                {
                    ChangeState(GameStateType.PAUSE);
                    setted = true;
                }
                else if (UIManager.Instance.AnyWindowIsOpened)
                {
                    ChangeState(GameStateType.WINDOW_VIEW);
                    setted = true;
                }
            }
            if (!setted)
                ChangeState(GameStateType.PLAYING);
        }

        private void ChangeState(GameStateType gameState, bool force = false)
        {
            if (force == false && CurrentGameState == gameState)
                return;

            CurrentGameState = gameState;
            OnCurrentGameStated?.Invoke();
        }


        #region HANDLERS

        private void HandleSceneLoaded(Scene scene1, Scene scene2)
        {
            if (ScenesManager.Instance != null)
                switch (ScenesManager.Instance.GetCurrentSceneType())
                {
                    case SceneType.NONE:
                        SetStartingState();
                        break;
                    case SceneType.MENU:
                        ChangeState(GameStateType.PAUSE);
                        break;
                    case SceneType.GAMEPLAY:
                        SetStartingState();
                        break;
                    default:
                        break;
                }
        }

        private void HandleWindowOpened(UIWindowBase window)
        {
            if (window is UIPauseWindow)
                ChangeState(GameStateType.PAUSE);
            else
                ChangeState(GameStateType.WINDOW_VIEW);
        }

        private void HandleWindowClosed(UIWindowBase window)
        {
            if (UIManager.Instance.AnyWindowIsOpened == false)
            {
                ChangeState(GameStateType.PLAYING);
            }
        }

        #endregion

        #endregion
    }
}