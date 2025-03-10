using TMPro;
using UnityEngine;

namespace UI.HUD.FadingItem
{
    public class HitDamageInformationHUD : FadingHUDElement
    {
        #region VARIABLES

        [SerializeField] private TMP_Text value;

        #endregion

        #region METHODS

        public override void OnUpdate()
        {
            base.OnUpdate();
            transform.position += Vector3.up * Time.deltaTime * 3;
        }

        public void SetValue(string newValue)
        {
            if (value != null)
                value.SetText(newValue);
        }

        #endregion
    }
}