using Gameplay.Scenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIController : MonoBehaviour
{
    #region VARIABLES

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public void PlayGame()
    {
        ScenesManager.Instance.LoadGameplayScene();
    }

    public void Exit()
    {
        Application.Quit();
    }

    #endregion
}
