using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    public class HealthBarHUD : FadingHUDElement
    {
        #region VARIABLES

        [SerializeField] private Slider slider;

        #endregion

        #region METHODS

        public void UpdateStatus(float percentage)
        {
            timer = liveTime;
            slider.value = percentage;
        }

        #endregion
    }
}