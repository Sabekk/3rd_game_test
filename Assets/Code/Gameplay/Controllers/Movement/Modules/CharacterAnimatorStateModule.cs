using Gameplay.Controller.Module;
using UnityEngine;

namespace Gameplay.Character.Movement
{
    public class CharacterAnimatorStateModule : ControllerModuleBase
    {
        #region VARIABLES

        [SerializeField] private float animationBlendingSpeed = 5;

        private float dirX;
        private float dirY;

        private float currentDirX;
        private float currentDirY;

        #endregion

        #region PROPERTIES

        public Animator CharacterAnimator => Character.CharacterInGame.Aniamtor;
        private int MoveDirectionX => Animator.StringToHash("MoveDirX");
        private int MoveDirectionY => Animator.StringToHash("MoveDirY");

        #endregion

        #region METHODS

        public override void OnUpdate()
        {
            base.OnUpdate();

            currentDirX = Mathf.MoveTowards(currentDirX, dirX, animationBlendingSpeed * Time.deltaTime);
            currentDirY = Mathf.MoveTowards(currentDirY, dirY, animationBlendingSpeed * Time.deltaTime);

            SetMovementAnimation(currentDirX, currentDirY);
        }

        protected virtual void MoveInDirection(Vector2 direction)
        {
            dirX = direction.x;
            dirY = direction.y;
        }

        private void SetMovementAnimation(float xDir, float yDir)
        {
            if (CharacterAnimator == null)
                return;

            CharacterAnimator.SetFloat(MoveDirectionX, xDir);
            CharacterAnimator.SetFloat(MoveDirectionY, yDir);
        }

        #endregion
    }
}