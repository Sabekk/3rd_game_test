using ObjectPooling;
using UI.HUD;
using UI.HUD.FadingItem;
using UnityEngine;

namespace Gameplay.Targeting
{
    public abstract class DamageTarget : MonoBehaviour, IDamagable
    {
        #region VARIABLES

        [SerializeField] private bool useHealthBar;
        [SerializeField] private bool showDamageValue;

        private HealthBarHUD healthBar;

        const string HEALTH_BAR = "HealthBar_HUD";
        const string HIT_INFORMATION = "HitInformation_HUD";

        const string HUD_CATEGORY = "HUD";

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
                    healthBar = ObjectPool.Instance.GetFromPool(HEALTH_BAR, HUD_CATEGORY).GetComponent<HealthBarHUD>();
                    healthBar.Initiliaze(transform);
                    healthBar.OnDisposed += HandleHealthBarRemoved;
                }

                healthBar.UpdateStatus(Health / MaxHealth);
            }

            if (showDamageValue)
            {
                HitDamageInformationHUD hitInformation = ObjectPool.Instance.GetFromPool(HIT_INFORMATION, HUD_CATEGORY).GetComponent<HitDamageInformationHUD>();
                hitInformation.Initiliaze();
                hitInformation.SetValue(damage.ToString());
                hitInformation.transform.position = transform.position;
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

        private void HandleHealthBarRemoved()
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