using Gameplay.Character.Movement;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay.Character
{
    public class Player : CharacterBase
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Controllers")] private PlayerMovementController movementController;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        protected override void SetControllers()
        {
            controllers = new();
            controllers.Add(movementController);
        }

        #endregion
    }
}
