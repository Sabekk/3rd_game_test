using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.GameStates
{
    public class GameStateManager : MonoSingleton<GameStateManager>
    {
        #region ACTION

        public event Action OnCurrentGameStated;

        #endregion

        #region VARIABLES

        #endregion

        #region PROPERTIES

        public GameStateType CurrentGameState { get; private set; }

        #endregion

        #region METHODS

        public void ChangeState(GameStateType gameState)
        {
            if (CurrentGameState == gameState)
                return;

            CurrentGameState = gameState;
            OnCurrentGameStated?.Invoke();
        }

        #endregion
    }
}