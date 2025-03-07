using Gameplay.Controller.Module;
using Gameplay.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Equipment
{
    public class EquipmentModule : ControllerModuleBase
    {
        #region ACTIONS

        public event Action<Item> OnItemEquiped;
        public event Action<Item> OnItemUnequiped;
        /// <summary>
        /// New equipped item | old equipped item
        /// </summary>
        public event Action<Item, Item> OnItemsReplaced;

        #endregion

        #region VARIABLES

        [SerializeField] private Dictionary<ItemCategory, Item> equippedItems;

        #endregion

        #region PROPERTIES

        public Dictionary<ItemCategory, Item> EquippedItems
        {
            get
            {
                if (equippedItems == null)
                    equippedItems = new();
                return equippedItems;
            }
        }

        #endregion

        #region METHODS

        public bool IsEquiped(Item item)
        {
            if (IsItemTypeEquiped(item.Category, out Item equipedItem))
                return equipedItem.IdEquals(item.Id);

            return false;
        }

        public bool IsItemTypeEquiped(ItemCategory type, out Item equipedItem)
        {
            return EquippedItems.TryGetValue(type, out equipedItem);
        }

        protected override void AttachEvents()
        {
            base.AttachEvents();
            Character.EquipmentController.OnItemsReplace += HandleItemsReplace;
            Character.EquipmentController.OnItemEquip += HandleItemEquip;
            Character.EquipmentController.OnItemUnequip += HandleItemUnequip;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Character.EquipmentController.OnItemsReplace -= HandleItemsReplace;
            Character.EquipmentController.OnItemEquip -= HandleItemEquip;
            Character.EquipmentController.OnItemUnequip -= HandleItemUnequip;
        }

        private void EquipItem(Item item)
        {
            if (EquippedItems.TryAdd(item.Category, item))
                OnItemEquiped?.Invoke(item);
        }

        private void UneqipItem(Item item)
        {
            if (EquippedItems.ContainsKey(item.Category))
            {
                EquippedItems.Remove(item.Category);
                OnItemUnequiped?.Invoke(item);
            }
        }

        #region HANDLERS

        private void HandleItemsReplace(Item itemToEquip, Item equippedItem)
        {
            if (EquippedItems.ContainsKey(equippedItem.Category))
            {
                EquippedItems.Remove(equippedItem.Category);
                if (EquippedItems.TryAdd(itemToEquip.Category, itemToEquip))
                {
                    OnItemsReplaced?.Invoke(itemToEquip, equippedItem);
                }
            }
            else
            {
                Debug.LogError($"Item was not equipped {equippedItem.ItemName}");
                EquipItem(itemToEquip);
            }
        }

        private void HandleItemEquip(Item item)
        {
            EquipItem(item);
        }

        private void HandleItemUnequip(Item item)
        {
            UneqipItem(item);
        }

        #endregion

        #endregion
    }
}