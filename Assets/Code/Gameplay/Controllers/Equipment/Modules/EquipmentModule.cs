using Gameplay.Controller.Module;
using Gameplay.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Equipment
{
    public class EquipmentModule : ControllerModuleBase
    {
        #region VARIABLES

        [SerializeField] private Dictionary<ItemCategory, Item> equippedItems;

        #endregion

        #region PROPERTIES

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
            return equippedItems.TryGetValue(type, out equipedItem);
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
            equippedItems.TryAdd(item.Category, item);
        }

        private void HandleItemUnequip(Item item)
        {
            equippedItems.Remove(item.Category);
        }

        #endregion

        #endregion
    }
}