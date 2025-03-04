using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public class CharacterController
    {
        #region VARIABLES

        [SerializeField, HideInInspector] private CharacterBase character;

        #endregion

        #region PROPERTIES

        public CharacterBase Character => character;

        #endregion

        #region METHODS

        public virtual void Initialize(CharacterBase character)
        {
            this.character = character;
        }

        public virtual void CleanUp() { }
        public virtual void OnUpdate() { }
        public virtual void AttachEvents() { }
        public virtual void DetachEvents() { }

        #endregion
    }
}