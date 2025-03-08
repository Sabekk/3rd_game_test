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
            return 100;
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