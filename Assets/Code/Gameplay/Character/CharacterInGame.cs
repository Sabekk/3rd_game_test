using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public class CharacterInGame : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private Rigidbody rb;
        [SerializeField] private CapsuleCollider capsuleCollider;
        [SerializeField] private Animator aniamtor;

        #endregion

        #region PROPERTIES

        public Rigidbody Rb => rb;
        public CapsuleCollider CapsuleCollider => capsuleCollider;
        public Animator Aniamtor => aniamtor;

        #endregion

        #region METHODS

        #endregion
    }
}
