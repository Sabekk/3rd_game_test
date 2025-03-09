using ObjectPooling;
using UI.HUD;
using UnityEngine;

namespace Gameplay.Targeting
{
    public abstract class DamageTarget : MonoBehaviour, IDamagable
    {
        #region VARIABLES

        [SerializeField] private bool useHealthBar;

        private HealthBarHUD healthBar;

        const string HEALTH_BAR = "HUD_healthBar";

        #endregion

        #region PROPERTIES

        public abstract float Health { get; set; }
        public abstract float MaxHealth { get; }
        public abstract bool IsKilled { get; set; }
        public bool IsAlive => Health > 0;

        #endregion

        #region METHODS


        public void TakeDamage(float damage)
        {
            if (IsKilled)
                return;

            Health -= damage;

            if (useHealthBar)
            {
                if (!healthBar)
                {
                    healthBar = ObjectPool.Instance.GetFromPool(HEALTH_BAR, "HUD").GetComponent<HealthBarHUD>();
                    healthBar.Initiliaze(transform);
                    healthBar.OnDisposed += HandleHealthBarRemoved;
                }

                healthBar.UpdateStatus(Health / MaxHealth);
            }

            if (!IsAlive)
                Kill();
        }

        public virtual void Kill()
        {
            IsKilled = true;
            if (useHealthBar && healthBar)
                healthBar.Dispose();
        }

        #region HANDLERS

        void HandleHealthBarRemoved()
        {
            if (healthBar)
            {
                healthBar.OnDisposed -= HandleHealthBarRemoved;
                this.healthBar = null;
            }
        }

        #endregion

        #endregion
    }
}