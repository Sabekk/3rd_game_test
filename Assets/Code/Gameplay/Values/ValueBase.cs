using UnityEngine;

namespace Gameplay.Values
{
    public abstract class ValueBase
    {
        #region VARIABLES

        [SerializeField] private float rawValue;
        [SerializeField] private float convertedValue;
        [SerializeField] private ValueType valueType;

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Converted value to type
        /// </summary>
        public float Value
        {
            get { return convertedValue; }
            set { SetRawValue(value); }
        }

        /// <summary>
        /// Raw value without converting to value type
        /// </summary>
        public float RawValue => rawValue;
        public ValueType ValueType => valueType;

        #endregion

        #region CONSTRUCTORS

        public ValueBase() { }
        public ValueBase(float value, ValueType valueType)
        {
            this.valueType = valueType;
            rawValue = value;
            convertedValue = ConvfertValueToType(rawValue);
        }

        #endregion

        #region METHODS

        public virtual void SetRawValue(float value)
        {
            rawValue = value;
            convertedValue = ConvfertValueToType(rawValue);
        }
        public virtual void AddToRawValue(float value)
        {
            if (value == 0)
                return;

            rawValue += value;
            convertedValue = ConvfertValueToType(rawValue);
        }

        protected virtual float ConvfertValueToType(float value)
        {
            float convertedValue = value;

            switch (valueType)
            {
                case ValueType.OVERALL:
                    convertedValue = value;
                    break;
                case ValueType.PERCENTAGE:
                    convertedValue = value / 100f;
                    break;
                default:
                    break;
            }

            return convertedValue;
        }

        #endregion
    }
}