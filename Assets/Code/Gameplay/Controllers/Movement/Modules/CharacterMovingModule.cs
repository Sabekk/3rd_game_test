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
        [SerializeField, FoldoutGroup("Values")] protected float moveAcceleration = 0.25f;

        protected Vector2 direction = Vector3.zero;
        protected Vector2 lookDirection = Vector3.zero;
        protected Vector3 moveDirection = Vector3.zero;

        #endregion

        #region PROPERTIES

        public bool IsMoving => direction != Vector2.zero;
        public bool IsGrounded => CharacterTransform == null ? false : Physics.Raycast(CharacterTransform.position, Vector3.down, Character.CharacterInGame.CharacterController.height * 0.5f + 0.2f);
        protected Transform CharacterTransform => Character.CharacterInGame ? Character.CharacterInGame.transform : null;
        protected CharacterController CharacterController => Character.CharacterInGame != null ? Character.CharacterInGame.CharacterController : null;

        #endregion

        #region METHODS

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (CharacterTransform == null)
                return;

            MoveCharacter();
            //RotateCharacterByLookDirection();
        }

        protected virtual void MoveCharacter()
        {
            if (IsMoving)
            {
                moveDirection = CharacterTransform.right * direction.x + CharacterTransform.forward * direction.y;
                Debug.Log(moveDirection);
                CharacterController?.Move(moveDirection * Time.deltaTime * walkSpeed);
            }

            if (!IsGrounded)
            {
                Vector3 velocity = Vector3.down * gravity * Time.deltaTime;
                CharacterController?.Move(velocity);
            }
        }

        protected virtual void MoveInDirection(Vector2 direction)
        {
            this.direction = direction;
        }

        protected virtual void LookInDirection(Vector2 direction)
        {
            this.lookDirection = direction;
        }

        #endregion
    }
}