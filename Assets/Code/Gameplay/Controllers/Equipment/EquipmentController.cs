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

        public Action<Item> OnItemCollect;
        public Action<Item> OnItemRemove;
        public Action<Item> OnItemEquip;
        public Action<Item> OnItemUnequip;

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

            modules.Add(inventoryModule);
            modules.Add(equipmentModule);
            modules.Add(visualizationModule);
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
                UnequipItem(equipedItem);

            OnItemEquip?.Invoke(item);
        }

        public void UnequipItem(Item item)
        {
            if (!IsEquiped(item))
                return;

            OnItemUnequip?.Invoke(item);
        }

        public void CollectItem(Item item)
        {
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
