using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public class CharacterController
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public virtual void Initialize() { }
        public virtual void CleanUp() { }
        public virtual void AttachEvents() { }
        public virtual void DetachEvents() { }

        #endregion
    }
}