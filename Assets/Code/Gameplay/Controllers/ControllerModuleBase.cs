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
        }

        public virtual void CleanUp()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void AttachEvents()
        {

        }

        public virtual void DetachEvents()
        {

        }

        #endregion
    }
}
