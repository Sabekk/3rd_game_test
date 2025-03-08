using Gameplay.Character;
using ObjectPooling;
using UnityEngine;

namespace Gameplay.Items
{
    public abstract class ItemVisualizationBase : MonoBehaviour, IPoolable
    {
        #region VARIABLES

        [SerializeField, Tooltip("Interract with objects until collision")] private bool interracting;

        #endregion

        #region PROPERTIES

        public PoolObject Poolable { get; set; }
        public CharacterBase Owner { get; set; }
        public Item Item { get; set; }

        #endregion

        #region UNITY_METHODS

        private void OnTriggerEnter(Collider collider)
        {
            if (interracting == false)
                return;


            if (collider.gameObject == Owner.CharacterInGame.gameObject)
                return;

            OnCollisionInterract(collider);
        }

        //private void OnCollisionEnter(Collision collision)
        //{
        //    if (interracting == false)
        //        return;

        //    if (collision.gameObject == Owner.CharacterInGame)
        //        return;

        //    OnCollisionInterract(collision);
        //}

        #endregion

        #region METHODS

        protected abstract void OnCollisionInterract(Collider collider);

        public void Initialize(CharacterBase owner, Item item)
        {
            Owner = owner;
            Item = item;
        }

        public void AssignPoolable(PoolObject poolable)
        {
            Poolable = poolable;
        }

        #endregion
    }
}
