using Gameplay.Character;
using UnityEngine;

namespace Gameplay.Controller.Module
{
    public abstract class ControllerModuleBase
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
            AttachEvents();
        }

        public virtual void CleanUp()
        {
            DetachEvents();
        }

        public virtual void OnUpdate()
        {

        }

        protected virtual void AttachEvents()
        {

        }
        protected virtual void DetachEvents()
        {

        }

        #endregion
    }
}
