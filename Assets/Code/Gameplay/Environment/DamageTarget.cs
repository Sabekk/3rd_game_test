namespace Gameplay.Targeting
{
    public abstract class DamageTarget : IDamagable
    {
        #region VARIABLES

        #endregion

        #region PROPERTIES

        public abstract float Health { get; set; }
        public abstract float MaxHealth { get; }
        public abstract bool IsKilled { get; set; }
        public bool IsAlive =>  Health > 0;

        #endregion

        #region METHODS


        public void TakeDamage(float damage)
        {
            if (IsKilled)
                return;

            Health -= damage;

            if (!IsAlive)
                Kill();
        }

        public virtual void Kill()
        {
            IsKilled = true;
        }

        #endregion
    }
}