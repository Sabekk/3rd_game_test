using UnityEngine;

namespace Gameplay.Timing
{
    public class TimeManager : GameplayManager<TimeManager>
    {
        #region VARIABLES

        [SerializeField] private float defaultTimeScale;

        private float previousTimeScale;

        #endregion

        #region PROPERTIES

        private bool TimeIsStopped => Time.timeScale == 0;

        #endregion

        #region METHODS

        public override void CleanUp()
        {
            Time.timeScale = defaultTimeScale;
            base.CleanUp();
        }

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
