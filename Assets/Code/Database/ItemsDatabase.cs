using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

        public Sprite TryGetItemCategoryIcon(ItemCategory category)
        {
            ItemCategoryIcons? itemCategoryIcon = itemIcons.Find(x => x.itemCategory == category);
            return itemCategoryIcon != null ? itemCategoryIcon?.categoryIcon : missingItemIcon;
        }

        public Sprite TryGetItemInCategoryIcon(ItemType itemType, ItemCategory category)
        {
            ItemCategoryIcons? itemCategoryIcon = itemIcons.Find(x => x.itemCategory == category);
            if (itemCategoryIcon != null)
            {
                ItemInCategoryIcons? itemInCategory = itemCategoryIcon?.itemsInCategory.Find(x => x.itemType == itemType);
                if (itemInCategory != null)
                    return itemInCategory?.icon;
                else
                    return missingItemIcon;
            }
            else
                return missingItemIcon;

        }

        [Button]
        private void RebuildIconsDatabase()
        {
            itemIcons.Clear();

            foreach (ItemCategory category in Enum.GetValues(typeof(ItemCategory)))
            {
                List<ItemInCategoryIcons> itemsInCategory = new();

                foreach (ItemType itemType in Enum.GetValues(typeof(ItemType)))
                {
                    Sprite icon = null;
                    string itemName = itemType.ToString() + category.ToString();
                    var itemGuids = AssetDatabase.FindAssets($"{itemName} t:Sprite");
                    if (itemGuids != null && itemGuids.Length > 0)
                    {
                        string path = AssetDatabase.GUIDToAssetPath(itemGuids[0]);
                        icon = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                    }

                    ItemInCategoryIcons itemInCategory = new ItemInCategoryIcons
                    {
                        itemType = itemType,
                        icon = icon
                    };

                    itemsInCategory.Add(itemInCategory);
                }

                ItemCategoryIcons categoryItems = new ItemCategoryIcons
                {
                    itemCategory = category,
                    itemsInCategory = itemsInCategory
                };
                itemIcons.Add(categoryItems);
            }
        }

        #endregion

        #region STRUCTS

        [Serializable]
        public struct ItemCategoryIcons
        {
            public ItemCategory itemCategory;
            public Sprite categoryIcon;
            public List<ItemInCategoryIcons> itemsInCategory;
        }

        [Serializable]
        public struct ItemInCategoryIcons
        {
            public ItemType itemType;
            public Sprite icon;
        }

        #endregion

    }
}