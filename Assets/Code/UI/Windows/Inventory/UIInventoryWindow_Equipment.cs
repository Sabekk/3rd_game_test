using Gameplay.Character;
using Gameplay.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Window.Inventory
{
    public class UIInventoryWindow_Equipment : UIWindowNested
    {
        #region VARIABLES

        [SerializeField] private List<EquipmentSlot> slots;

        private Dictionary<ItemCategory, EquipmentSlot> categorySlots;

        #endregion

        #region PROPERTIES

        private Player Player => CharacterManager.Instance.Player;

        #endregion

        #region METHODS

        public override void Initialize()
        {
            base.Initialize();
            categorySlots = new();

            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].Initialize(() => HandleSlotClick(slots[i]));

                if (categorySlots.TryAdd(slots[i].ItemSlotCategory, slots[i]) == false)
                    Debug.LogError($"Duplicated slots category in equipment slots. Check settings of prefab", this);
            }

            FullyRefreshSlots();
        }

        public override void CleanUp()
        {
            for (int i = 0; i < slots.Count; i++)
                slots[i].CleanUp();

            base.CleanUp();
        }

        protected override void Refresh()
        {
            base.Refresh();
            FullyRefreshSlots();
        }

        protected override void AttachEvents()
        {
            base.AttachEvents();
            if (Player != null)
            {
                Player.EquipmentController.OnItemEquipped += HandleItemEquiped;
                Player.EquipmentController.OnItemUnequipped += HandleItemUnequiped;
                Player.EquipmentController.OnItemsReplaced += HandleItemsReplaced;
            }
        }
        protected override void DetachEvents()
        {
            base.DetachEvents();
            if (Player != null)
            {
                Player.EquipmentController.OnItemEquipped -= HandleItemEquiped;
                Player.EquipmentController.OnItemUnequipped -= HandleItemUnequiped;
                Player.EquipmentController.OnItemsReplaced -= HandleItemsReplaced;
            }
        }

        private void FullyRefreshSlots()
        {
            if (Player == null)
                return;

            foreach (var item in Player.EquipmentController.EquipmentModule.EquippedItems)
                TrySetItemOnSlot(item.Key, item.Value);
        }

        private void TrySetItemOnSlot(ItemCategory category, Item item)
        {
            if (categorySlots.TryGetValue(category, out EquipmentSlot slot))
                slot.SetItem(item);
        }

        #region HANDLERS

        private void HandleSlotClick(EquipmentSlot slot)
        {

        }

        private void HandleItemsReplaced(Item newEquipedItem, Item oldEquipedItem)
        {
            TrySetItemOnSlot(oldEquipedItem.Category, newEquipedItem);
        }

        private void HandleItemEquiped(Item item)
        {
            TrySetItemOnSlot(item.Category, item);
        }

        private void HandleItemUnequiped(Item item)
        {
            TrySetItemOnSlot(item.Category, null);
        }

        #endregion

        #endregion
    }
}