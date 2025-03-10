using Gameplay.Character;
using ObjectPooling;
using UnityEngine;

namespace Gameplay.Items
{
    [RequireComponent(typeof(Collider))]
    public abstract class ItemVisualizationBase : MonoBehaviour, IPoolable
    {
        #region VARIABLES

        [SerializeField, Tooltip("Collider for detecting collisions. Can be null for none detection")] private Collider visualizationCollider;
        [SerializeField, Tooltip("Interract with objects until collision")] private bool interracting;

        #endregion

        #region PROPERTIES

        public PoolObject Poolable { get; set; }
        public CharacterBase Owner { get; set; }
        public Item Item { get; set; }
        public Collider VisualizationCollider => visualizationCollider;

        #endregion

        #region UNITY_METHODS

        private void OnTriggerEnter(Collider collider)
        {
            if (interracting == false)
                return;

            if (collider == null)
                return;

            if (collider.gameObject == Owner.CharacterInGame.gameObject)
                return;

            OnCollisionInterract(collider);
        }

        #endregion

        #region METHODS

        protected abstract void OnCollisionInterract(Collider collider);
        public virtual void SetColliderState(bool state)
        {
            if (VisualizationCollider == null)
                return;

            VisualizationCollider.enabled = state;
        }

        public void Initialize(CharacterBase owner, Item item)
        {
            Owner = owner;
            Item = item;

            SetColliderState(owner.AttackingController.IsAttacking);
        }

        public void AssignPoolable(PoolObject poolable)
        {
            Poolable = poolable;
        }

        #endregion
    }
}
