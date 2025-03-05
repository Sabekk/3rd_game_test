using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public class CharacterInGame : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private Animator aniamtor;
        [SerializeField] private CharacterController characterController;

        #endregion

        #region PROPERTIES

        public Animator Aniamtor => aniamtor;
        public CharacterController CharacterController => characterController;

        #endregion

        #region METHODS

        #endregion
    }
}
