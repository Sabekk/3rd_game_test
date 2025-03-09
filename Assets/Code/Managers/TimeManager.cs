using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Timing
{
    public class TimeManager : MonoSingleton<TimeManager>
    {
        #region VARIABLES

        [SerializeField] private float defaultTimeScale;

        private float previousTimeScale;

        #endregion

        #region PROPERTIES

        private bool TimeIsStopped => Time.timeScale == 0;

        #endregion

        #region METHODS

        public void TryToggleTime(bool state)
        {
            if (TimeIsStopped && state == true)
            {
                Time.timeScale = previousTimeScale != 0 ? previousTimeScale : defaultTimeScale;
            }
            else if (!TimeIsStopped && state == false)
            {
                Time.timeScale = previousTimeScale;
                Time.timeScale = 0;
            }
        }

        #endregion
    }
}
