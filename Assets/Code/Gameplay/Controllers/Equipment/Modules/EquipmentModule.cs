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
            Character.EquipmentController.OnItemEquip += HandleItemEquip;
            Character.EquipmentController.OnItemUnequip += HandleItemUnequip;
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            Character.EquipmentController.OnItemEquip -= HandleItemEquip;
            Character.EquipmentController.OnItemUnequip -= HandleItemUnequip;
        }

        #region HANDLERS

        private void HandleItemEquip(Item item)
        {
            if (EquippedItems.TryAdd(item.Category, item))
                OnItemEquiped?.Invoke(item);
        }

        private void HandleItemUnequip(Item item)
        {
            if (EquippedItems.ContainsKey(item.Category))
            {
                EquippedItems.Remove(item.Category);
                OnItemUnequiped?.Invoke(item);
            }
        }

        #endregion

        #endregion
    }
}