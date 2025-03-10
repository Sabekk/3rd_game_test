using Gameplay.Character;
using Gameplay.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Attacking
{
    public class AttackingController : CharacterControllerBase
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        public bool IsAttacking => Character.MovementController.AnimatorModule.DuringAttackAnimation;
        public bool DuringAttack { get; set; }
        public bool CanAttack => true;

        #endregion

        #region METHODS

        public void SetAttackFrames()
        {
            DuringAttack = true;
            ToggleDamagableColliders(true);
        }

        public void DisableAttackFrames()
        {
            DuringAttack = false;
            ToggleDamagableColliders(false);
        }

        private void ToggleDamagableColliders(bool state)
        {
            if (Character.EquipmentController.EquipmentModule.EquippedItems.TryGetValue(ItemCategory.Weapon, out Item weapon))
            {
                foreach (var visualization in weapon.Visualizations)
                {
                    if (visualization is DamagableItemVisualization damagableVisualization)
                    {
                        damagableVisualization.SetColliderState(state);
                    }
                }
            }
        }

        #endregion
    }
}