using Gameplay.Character;
using Gameplay.Items;
using System;
using UnityEngine;

namespace Gameplay.Equipment
{
    [Serializable]
    public class EquipmentController : CharacterControllerBase
    {
        #region ACTIONS
        public event Action<Item> OnItemCollected;
        public event Action<Item> OnItemRemoved;
        public event Action<Item> OnItemEquipped;
        public event Action<Item> OnItemUnequipped;
        /// <summary>
        /// Item to equip | equipped item
        /// </summary>
        public event Action<Item, Item> OnItemsReplaced;

        #endregion

        #region VARIABLES

        [SerializeField] private InventoryModule inventoryModule;
        [SerializeField] private EquipmentModule equipmentModule;
        [SerializeField] private VisualizationModule visualizationModule;

        #endregion

        #region PROPERTIES

        public InventoryModule InventoryModule => inventoryModule;
        public EquipmentModule EquipmentModule => equipmentModule;
        public VisualizationModule VisualizationModule => visualizationModule;

        #endregion

        #region METHODS

        public override void SetModules()
        {
            base.SetModules();

            modules.Add(inventoryModule = new());
            modules.Add(equipmentModule = new());
            modules.Add(visualizationModule = new());
        }

        public bool IsEquiped(Item item)
        {
            return EquipmentModule.IsEquiped(item);
        }

        public bool IsItemTypeEquiped(ItemCategory type, out Item equipedItem)
        {
            return EquipmentModule.IsItemTypeEquiped(type, out equipedItem);
        }

        public void EquipItem(Item item)
        {
            if (!InventoryModule.ContainItem(item))
                return;

            if (IsItemTypeEquiped(item.Category, out Item equipedItem))
            {
                ReplaceItems(item, equipedItem);
            }
            else
            {
                InventoryModule.RemoveItem(item, false);
                EquipmentModule.EquipItem(item);
                OnItemEquipped?.Invoke(item);
            }
        }

        public void UnequipItem(Item item)
        {
            if (InventoryModule.CanAddItem() == false)
                return;

            if (EquipmentModule.UneqipItem(item))
            {
                InventoryModule.AddItem(item);
                OnItemUnequipped?.Invoke(item);
            }
        }

        private void ReplaceItems(Item itemToEquip, Item equippedItem)
        {
            EquipmentModule.UneqipItem(equippedItem);
            EquipmentModule.EquipItem(itemToEquip);

            if (InventoryModule.ReplaceItems(equippedItem, itemToEquip) == false)
            {
                InventoryModule.RemoveItem(itemToEquip, false);
                InventoryModule.AddItem(equippedItem);
            }

            OnItemsReplaced?.Invoke(itemToEquip, equippedItem);
        }

        public void CollectItem(Item item)
        {
            if (InventoryModule.AddItem(item, true))
                OnItemCollected?.Invoke(item);
        }

        public void RemoveItem(Item item)
        {
            if (InventoryModule.RemoveItem(item))
                OnItemRemoved?.Invoke(item);
        }
        #endregion
    }
}
