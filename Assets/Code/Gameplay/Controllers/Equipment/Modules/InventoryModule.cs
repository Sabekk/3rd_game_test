using Gameplay.Character;
using Gameplay.Controller.Module;
using Gameplay.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Equipment
{
    public class InventoryModule : ControllerModuleBase
    {
        #region ACTIONS

        public event Action<Item> OnItemCollected;
        public event Action<Item> OnItemRemoved;
        /// <summary>
        /// New item in inventory | old item in inventory
        /// </summary>
        public event Action<Item, Item> OnItemsReplaced;

        #endregion

        #region VARIABLES

        [SerializeField] private List<Item> itemsInventory;

        #endregion

        #region PROPERTIES

        public List<Item> ItemsInventory
        {
            get
            {
                if (itemsInventory == null)
                    itemsInventory = new();
                return itemsInventory;
            }
        }
        public int EmptySlots => MaxItemsCount - ItemsInventory.Count;
        private int MaxItemsCount => Character.Data.MaxInventorySlots;

        #endregion

        #region METHODS

        public bool ContainItem(Item item)
        {
            return ItemsInventory.ContainsId(item.Id);
        }

        public bool CanAddItem()
        {
            return EmptySlots > 0;
        }

        protected override void AttachEvents()
        {
            base.AttachEvents();
            Character.EquipmentController.OnItemEquip += HandleItemEquip;
            Character.EquipmentController.OnItemUnequip += HandleItemUnequip;

            Character.EquipmentController.OnItemCollect += HandleItemCollect;
            Character.EquipmentController.OnItemRemove += HandleItemRemove;

            Character.EquipmentController.OnItemsReplace += HandleItemsReplace;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Character.EquipmentController.OnItemEquip -= HandleItemEquip;
            Character.EquipmentController.OnItemUnequip -= HandleItemUnequip;

            Character.EquipmentController.OnItemCollect -= HandleItemCollect;
            Character.EquipmentController.OnItemRemove -= HandleItemRemove;

            Character.EquipmentController.OnItemsReplace -= HandleItemsReplace;
        }

        private void AddItem(Item item)
        {
            ItemsInventory.Add(item);
            OnItemCollected?.Invoke(item);
        }

        private void RemoveItem(Item item)
        {
            if (ItemsInventory.ContainsId(item.Id))
            {
                ItemsInventory.Remove(item);
                OnItemRemoved?.Invoke(item);
            }
        }

        #region HANDLERS

        private void HandleItemsReplace(Item itemToEquip, Item equipedItem)
        {
            int indexOfReplacingItem = ItemsInventory.IndexOf(itemToEquip);
            if (indexOfReplacingItem >= 0)
            {
                ItemsInventory.RemoveAt(indexOfReplacingItem);
                ItemsInventory.Insert(indexOfReplacingItem, equipedItem);
                OnItemsReplaced?.Invoke(equipedItem, itemToEquip);
            }
            else
            {
                Debug.LogError($"Item didnt found in inventory {equipedItem.ItemName}");
                RemoveItem(itemToEquip);
                AddItem(equipedItem);
            }
        }

        private void HandleItemEquip(Item item)
        {
            RemoveItem(item);
        }

        private void HandleItemUnequip(Item item)
        {
            AddItem(item);
        }

        private void HandleItemCollect(Item item)
        {
            AddItem(item);
        }

        private void HandleItemRemove(Item item)
        {
            RemoveItem(item);
        }

        #endregion

        #endregion
    }
}