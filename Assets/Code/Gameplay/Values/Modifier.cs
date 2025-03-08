using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Values
{
    public class Modifier
    {
        #region VARIABLES

        [SerializeField] private float modifierValue;
        [SerializeField] private ModifierType modifierType;

        #endregion

        #region PROPERTIES

        public float ModifierValue => modifierValue;
        public ModifierType ModifierType => modifierType;

        #endregion

        #region CONSTRUCTORS

        public Modifier() { }
        public Modifier(ModifierType modifierType, float modifierValue)
        {
            this.modifierType = modifierType;

            switch (modifierType)
            {
                case ModifierType.ADD:
                    this.modifierValue = modifierValue;
                    break;
                case ModifierType.PERCENTAGE_ADD:
                    this.modifierValue = modifierValue / 100f;
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}