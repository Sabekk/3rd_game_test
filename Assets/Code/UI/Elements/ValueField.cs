using Gameplay.Values;
using TMPro;
using UnityEngine;

namespace UI.BaseElements
{
    public class ValueField : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private string preffixText;
        [SerializeField] private TextMeshProUGUI showingText;

        private ModifiableValue modifiableValue;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public void Initialize(ModifiableValue modifiableValue)
        {
            this.modifiableValue = modifiableValue;
            RefreshValue();

            AttachEvents();
        }

        public void CleanUp()
        {
            DetachEvents();
            modifiableValue = null;
        }

        private void RefreshValue()
        {
            if (string.IsNullOrEmpty(preffixText))
                showingText.SetText(modifiableValue.GetValueToShow());
            else
                showingText.SetText(string.Format("{0} {1}", preffixText, modifiableValue.GetValueToShow()));
        }

        private void AttachEvents()
        {
            if (modifiableValue != null)
            {
                modifiableValue.OnValueChanged += HandleValueChange;
            }
        }

        private void DetachEvents()
        {
            if (modifiableValue != null)
            {
                modifiableValue.OnValueChanged -= HandleValueChange;
            }
        }

        #region HANDLERS

        private void HandleValueChange()
        {
            RefreshValue();
        }

        #endregion

        #endregion
    }
}
