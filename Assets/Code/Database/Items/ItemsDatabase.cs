using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Database.Items
{
    [CreateAssetMenu(menuName = "Database/ItemsDatabase", fileName = "ItemsDatabase")]
    public class ItemsDatabase : ScriptableObject
    {
        #region VARIABLES

        [SerializeField] private List<ItemsCategoryData> itemCategories;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS


        public ItemsCategoryData GetCategory(ItemCategory category)
        {
            return itemCategories.Find(x => x.ItemCategory == category);
        }

        public ItemData GetItemData(ItemType itemType, ItemCategory category)
        {
            ItemsCategoryData categoryData = GetCategory(category);
            if (categoryData == null)
                return null;

            return categoryData.GetItemData(itemType);
        }

        public ItemData GetItemData(int itemDataId, ItemCategory category)
        {
            ItemsCategoryData categoryData = GetCategory(category);
            if (categoryData == null)
                return null;

            return categoryData.GetItemData(itemDataId);
        }

        [Button]
        private void RebuildIconsDatabase()
        {
            itemCategories.Clear();

            foreach (ItemCategory category in Enum.GetValues(typeof(ItemCategory)))
            {
                List<ItemData> itemsInCategory = new();

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

                    ItemData itemInCategory = new ItemData(itemType, icon);

                    itemsInCategory.Add(itemInCategory);
                }

                ItemsCategoryData categoryItems = new ItemsCategoryData(category, null, itemsInCategory);
                itemCategories.Add(categoryItems);
            }
        }

        #endregion
    }
}