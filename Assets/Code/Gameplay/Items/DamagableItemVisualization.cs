using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    public class DamagableItemVisualization : ItemVisualizationBase, IDamageDealer
    {
        #region VARIABLES

        private HashSet<IDamagable> hittedObjectsDuringAttack = new HashSet<IDamagable>();

        #endregion

        #region METHODS

        public override void SetColliderState(bool state)
        {
            base.SetColliderState(state);
            if (state == true)
                hittedObjectsDuringAttack.Clear();
        }

        public void DealDamage(IDamagable target)
        {
            if (hittedObjectsDuringAttack.Contains(target))
                return;

            if (!target.IsKilled)
            {
                target.TakeDamage(GetDamageToDeal());
                hittedObjectsDuringAttack.Add(target);
            }
        }

        public float GetDamageToDeal()
        {
            bool isCritical = UnityEngine.Random.Range(0, 100) < Owner.ValuesController.CharacterValues.CritChance.CurrentRawValue;
            return Owner.ValuesController.CharacterValues.Damage.CurrentValue * (isCritical ? 2f : 1f);
        }

        protected override void OnCollisionInterract(Collider collider)
        {
            if (Owner.AttackingController.IsAttacking == false)
                return;

            IDamagable damagable = collider.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
                DealDamage(damagable);
        }

        #endregion
    }
}