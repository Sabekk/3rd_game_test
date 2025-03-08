using ObjectPooling;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Targeting
{
    public class DamagableProp : MonoBehaviour, IDamagable
    {
        #region VARIABLES

        [SerializeField] private float HP;
        [SerializeField] private bool spawnDestroyedPrefab;
        [SerializeField, ShowIf(nameof(spawnDestroyedPrefab))] private int itemsCount = 1;
        [SerializeField, ShowIf(nameof(spawnDestroyedPrefab)), ValueDropdown(ObjectPoolDatabase.GET_POOL_PROPS_METHOD)] private int destroyedPropId;

        private float currentHp;

        const string PROP_POOL_CATEGORY = "Prop";

        #endregion

        #region PROPERTIES

        public float Health
        {
            get { return currentHp; }
            set { currentHp = value; }
        }
        public float MaxHealth => HP;
        public bool IsKilled { get; set; }
        public bool IsAlive => Health > 0;

        #endregion

        #region UNITY_METHODS

        private void Start()
        {
            if (IsKilled == false)
                Health = MaxHealth;
        }

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
            if (IsKilled)
                return;

            IsKilled = true;

            // TODO Change to effects after death
            if (spawnDestroyedPrefab)
            {
                GameObject destroyedProp = ObjectPool.Instance.GetFromPool(destroyedPropId, PROP_POOL_CATEGORY).Prefab;
                Vector3 pos = transform.position;
                Quaternion rotation = transform.rotation;

                Destroy(gameObject);

                destroyedProp.transform.position = pos;
                destroyedProp.transform.rotation = rotation;
            }
        }

        #endregion
    }
}
