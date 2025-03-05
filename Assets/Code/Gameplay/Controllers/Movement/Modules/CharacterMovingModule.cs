using Gameplay.Controller.Module;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Movement
{
    public class CharacterMovingModule : ControllerModuleBase
    {
        #region VARIABLES

        [SerializeField, FoldoutGroup("Values")] protected float walkSpeed = 4;
        [SerializeField, FoldoutGroup("Values")] protected float runSpeed = 7;
        [SerializeField, FoldoutGroup("Values")] protected float jumpPower = 10;
        [SerializeField, FoldoutGroup("Values")] protected float gravity = 5;
        [SerializeField, FoldoutGroup("Values")] protected float rotationSpeed = 25;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        #endregion
    }
}