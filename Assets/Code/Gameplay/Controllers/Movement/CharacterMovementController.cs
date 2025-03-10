using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Movement
{
    public class CharacterMovementController : CharacterControllerBase
    {
        #region VARIABLES

        [SerializeField] protected CharacterAnimatorStateModule animatorModule;
        [SerializeField] protected CharacterMovingModule movingModule;

        #endregion

        #region PROPERTIES

        public CharacterAnimatorStateModule AnimatorModule => animatorModule;
        public CharacterMovingModule MovingModule => movingModule;

        #endregion

        #region METHODS

        public override void CreateModules()
        {
            base.CreateModules();
            animatorModule = new CharacterAnimatorStateModule();
            movingModule = new CharacterMovingModule();
        }

        public override void SetModules()
        {
            base.SetModules();
            modules.Add(animatorModule);
            modules.Add(movingModule);
        }

        #endregion
    }
}
