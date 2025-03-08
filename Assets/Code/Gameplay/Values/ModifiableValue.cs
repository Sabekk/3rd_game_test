using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Values
{
    public class ModifiableValue
    {
        #region ACTIONS

        public event Action OnValueChanged;

        #endregion

        #region VARIABLES

        [SerializeField] private float rawValue;
        [SerializeField] private float currentValue;
        [SerializeField] private List<ModifiableValue> additionalComponents;
        [SerializeField] private List<Modifier> modifiers;
        [SerializeField] private bool needRefresh;

        #endregion

        #region PROPERTIES

        public float CurrentValue
        {
            get
            {
                if (needRefresh)
                    RecalculateCurrentValue();
                return currentValue;
            }
        }

        #endregion

        #region CONSTRUCTORS

        public ModifiableValue() { }
        public ModifiableValue(float startingValue)
        {
            additionalComponents = new();
            modifiers = new();
            SetBaseValue(startingValue);
        }

        #endregion

        #region METHODS

        public void AddToBaseValue(float value)
        {
            rawValue += value;
            if (value != 0)
            {
                needRefresh = true;
                OnValueChanged?.Invoke();
            }
        }

        public void SetBaseValue(float baseValue)
        {
            rawValue = baseValue;
            needRefresh = true;
            OnValueChanged?.Invoke();
        }

        public void AddNewComponent(ModifiableValue newComponent)
        {
            additionalComponents.Add(newComponent);

            if (newComponent.CurrentValue != 0)
            {
                needRefresh = true;
                OnValueChanged?.Invoke();
            }
        }

        public void RemoveComponent(ModifiableValue componentToRemove)
        {
            additionalComponents.Remove(componentToRemove);

            if (componentToRemove.CurrentValue != 0)
            {
                needRefresh = true;
                OnValueChanged?.Invoke();
            }
        }

        public void AddModifier(Modifier modifier)
        {
            modifiers.Add(modifier);

            if (modifier.ModifierValue != 0)
            {
                needRefresh = true;
                OnValueChanged?.Invoke();
            }
        }

        public void RemoveModifier(Modifier modifier)
        {
            modifiers.Remove(modifier);

            if (modifier.ModifierValue != 0)
            {
                needRefresh = true;
                OnValueChanged?.Invoke();
            }
        }

        private void RecalculateCurrentValue()
        {
            float newValue = rawValue;

            for (int i = 0; i < additionalComponents.Count; i++)
                newValue += additionalComponents[i].CurrentValue;

            newValue = GetModifiedValue(newValue);
            currentValue = newValue;

            needRefresh = false;
        }

        private float GetModifiedValue(float valueToModify)
        {
            float modifiedValue = valueToModify;

            for (int i = 0; i < modifiers.Count; i++)
            {
                switch (modifiers[i].ModifierType)
                {
                    case ModifierType.ADD:
                        modifiedValue += modifiers[i].ModifierValue;
                        break;
                    case ModifierType.PERCENTAGE_ADD:
                        modifiedValue += valueToModify * modifiers[i].ModifierValue;
                        break;
                    default:
                        break;
                }
            }

            return modifiedValue;
        }

        #endregion
    }
}
