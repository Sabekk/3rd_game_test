using System;
using UnityEngine;

namespace Gameplay.Items
{
    public class DamagableItemVisualization : ItemVisualizationBase, IDamageDealer
    {
        #region METHODS

        public void DealDamage(IDamagable target)
        {
            if (!target.IsKilled)
                target.TakeDamage(GetDamageToDeal());
        }

        public float GetDamageToDeal()
        {
            bool isCritical = UnityEngine.Random.Range(0, 100) < Owner.ValuesController.CharacterValues.CritChance.CurrentRawValue;
            return Owner.ValuesController.CharacterValues.Damage.CurrentValue * (isCritical ? 2f : 1f);
        }

        protected override void OnCollisionInterract(Collider collider)
        {
            IDamagable damagable = collider.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
                DealDamage(damagable);
        }

        #endregion
    }
}