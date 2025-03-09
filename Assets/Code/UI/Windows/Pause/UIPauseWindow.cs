using Gameplay.Scenes;
using System.Collections;
using System.Collections.Generic;
using UI.Window;
using UnityEngine;

namespace UI.Window.Pause
{
    public class UIPauseWindow : UIWindowBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public void BackToMenu()
        {
            if (ScenesManager.Instance)
                ScenesManager.Instance.LoadMenuScene();
        }

        #endregion
    }
}
