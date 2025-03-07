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

        public event Action<Item> OnItemCollect;
        public event Action<Item> OnItemRemove;
        public event Action<Item> OnItemEquip;
        public event Action<Item> OnItemUnequip;
        /// <summary>
        /// Item to equip | equipped item
        /// </summary>
        public event Action<Item, Item> OnItemsReplace;

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

        public void InitializeActionToModules()
        {

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
                ReplaceItems(item, equipedItem);
            //UnequipItem(equipedItem);
            else
                OnItemEquip?.Invoke(item);
        }

        public void UnequipItem(Item item)
        {
            if (!IsEquiped(item))
                return;

            OnItemUnequip?.Invoke(item);
        }

        private void ReplaceItems(Item itemToEquip, Item equippedItem)
        {
            OnItemsReplace?.Invoke(itemToEquip, equippedItem);
        }

        public void CollectItem(Item item)
        {
            if (InventoryModule.CanAddItem())
                OnItemCollect?.Invoke(item);
        }

        public void RemoveItem(Item item)
        {
            UnequipItem(item);

            if (InventoryModule.ContainItem(item))
                OnItemRemove?.Invoke(item);
        }
        #endregion
    }
}
