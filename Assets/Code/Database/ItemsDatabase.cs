using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Character
{
    [CreateAssetMenu(menuName = "Database/ItemsDatabase", fileName = "ItemsDatabase")]
    public class ItemsDatabase : ScriptableObject
    {
        #region VARIABLES

        [SerializeField] private Sprite missingItemIcon;
        [SerializeField] private List<ItemCategoryIcons> itemIcons;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public Sprite TryGetItemIcon(ItemCategory category)
        {
            ItemCategoryIcons? itemCategoryIcon = itemIcons.Find(x => x.itemCategory == category);
            return itemCategoryIcon != null ? itemCategoryIcon?.icon : missingItemIcon;
        }

        #endregion

        #region STRUCTS

        [Serializable]
        public struct ItemCategoryIcons
        {
            public ItemCategory itemCategory;
            public Sprite icon;
        }

        #endregion

    }
}