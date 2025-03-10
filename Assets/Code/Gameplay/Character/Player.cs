using Gameplay.Character.Movement;

namespace Gameplay.Character
{
    public class Player : CharacterBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        public PlayerMovementController PlayerMovementController => movementController as PlayerMovementController;

        #endregion

        #region METHODS

        protected override void CreateControllers()
        {
            base.CreateControllers();
            movementController = new PlayerMovementController();
        }

        #endregion
    }
}
