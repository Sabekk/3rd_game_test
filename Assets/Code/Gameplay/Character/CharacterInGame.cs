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

        #endregion

        #region PROPERTIES

        public Rigidbody Rb => rb;
        public CapsuleCollider CapsuleCollider => capsuleCollider;

        #endregion

        #region METHODS

        #endregion
    }
}
