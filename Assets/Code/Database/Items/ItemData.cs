using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Database.Items
{
    [Serializable]
    public class ItemData : IIdEqualable
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private int id = Guid.NewGuid().GetHashCode();
        [SerializeField] private ItemType itemType;
        [SerializeField, ValueDropdown(ObjectPooling.ObjectPoolDatabase.GET_POOL_ITEMS_METHOD)] private int poolVisualizationId;
        [SerializeField] private Sprite icon;

        #endregion

        #region PROPERTIES

        public int Id => id;
        public ItemType ItemType => itemType;
        public int PoolVisualizationId => poolVisualizationId;
        public Sprite Icon => icon;

        #endregion

        #region CONSTRUCTORS

        public ItemData() { }
        public ItemData(ItemType itemType, Sprite icon)
        {
            this.itemType = itemType;
            this.icon = icon;
        }

        #endregion

        #region METHODS

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}