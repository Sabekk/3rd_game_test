using Database;
using Database.Items;
using System;
using UnityEngine;

namespace UI.Window.Inventory
{
    public class EquipmentSlot : SlotBase
    {
        #region VARIABLES

        [SerializeField] private ItemCategory itemSlotCategory;

        private Sprite categoryIcon;

        #endregion

        #region PROPERTIES

        public ItemCategory ItemSlotCategory => itemSlotCategory;

        #endregion

        #region METHODS

        public override void Initialize(Action onClickAction)
        {
            base.Initialize(onClickAction);

            ItemsCategoryData categoryData = MainDatabases.Instance.ItemsDatabase.GetCategory(ItemSlotCategory);
            if (categoryData == null)
            {
                Debug.LogError($"Missing category {ItemSlotCategory} in database");
                return;
            }

            categoryIcon = categoryData.CategoryIcon;

            RefreshItemInSlot();
        }

        protected override void SetIcon()
        {
            if (ItemInSlot != null)
                itemIcon.sprite = ItemInSlot.Data.Icon;
            else
                itemIcon.sprite = categoryIcon;
        }

        #endregion
    }
}