using System;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Items
{
    [Serializable]
    public class ItemsCategoryData
    {
        #region VARIABLES

        [SerializeField] private ItemCategory itemCategory;
        [SerializeField] private Sprite categoryIcon;
        [SerializeField] private ItemData defaultItemData;
        [SerializeField] private List<ItemData> itemsInCategory;

        #endregion

        #region PROPERTIES

        public ItemCategory ItemCategory => itemCategory;
        public Sprite CategoryIcon => categoryIcon;
        public List<ItemData> ItemsInCategory => itemsInCategory;
        public ItemData DefaultItemData => defaultItemData;

        #endregion

        #region CONSTRUCTORS

        public ItemsCategoryData()
        {

        }

        public ItemsCategoryData(ItemCategory itemCategory, Sprite categoryIcon, List<ItemData> itemsInCategory)
        {
            this.itemCategory = itemCategory;
            this.categoryIcon = categoryIcon;
            this.itemsInCategory = itemsInCategory;
        }

        #endregion

        #region METHODS

        public ItemData GetItemData(ItemType itemType)
        {
            ItemData itemData = itemsInCategory.Find(x => x.ItemType == itemType);
            return itemData == null ? DefaultItemData : itemData;
        }

        public ItemData GetItemData(int dataId)
        {
            ItemData itemData = itemsInCategory.Find(x => x.IdEquals(dataId));
            return itemData == null ? DefaultItemData : itemData;
        }

        #endregion
    }
}
