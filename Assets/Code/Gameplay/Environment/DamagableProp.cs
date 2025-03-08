using ObjectPooling;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Targeting
{
    [RequireComponent(typeof(Collider))]
    public class DamagableProp : DamageTarget, IDamagable
    {
        #region VARIABLES

        [SerializeField] private float HP;
        [SerializeField] private Collider objectCollider;
        [SerializeField] private bool spawnDestroyedPrefab;
        [SerializeField, ShowIf(nameof(spawnDestroyedPrefab))] private int itemsCount = 1;
        [SerializeField, ShowIf(nameof(spawnDestroyedPrefab)), ValueDropdown(ObjectPoolDatabase.GET_POOL_PROPS_METHOD)] private int destroyedPropId;

        private float currentHp;

        const string PROP_POOL_CATEGORY = "Prop";

        #endregion

        #region PROPERTIES

        public override float Health
        {
            get { return currentHp; }
            set { currentHp = value; }
        }
        public override float MaxHealth => HP;

        public override bool IsKilled { get; set; }

        #endregion

        #region UNITY_METHODS

        private void Awake()
        {
            if (objectCollider == null)
                objectCollider = GetComponent<Collider>();
        }

        private void Start()
        {
            if (IsKilled == false)
                Health = MaxHealth;
        }

        #endregion


        #region METHODS

        public override void Kill()
        {
            if (IsKilled)
                return;

            IsKilled = true;

            // TODO Change to effects after death
            if (spawnDestroyedPrefab)
            {
                for (int i = 0; i < itemsCount; i++)
                {
                    GameObject destroyedProp = ObjectPool.Instance.GetFromPool(destroyedPropId, PROP_POOL_CATEGORY).Prefab;
                    Vector3 pos = GetRandomPostion(objectCollider.bounds);
                    destroyedProp.transform.position = pos;
                }
            }
            Destroy(gameObject);
        }

        Vector3 GetRandomPostion(Bounds bounds)
        {
            return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z));
        }

        #endregion
    }
}
