using Gameplay.Character;
using Gameplay.Items;
using ObjectPooling;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Window.Inventory
{
    public class UIInventoryWindow_Inventory : UIWindowNested
    {
        #region VARIABLES

        [SerializeField, ValueDropdown(ObjectPoolDatabase.GET_POOL_UI_WINDOW_METHOD)] private int slotPrefabId;
        [SerializeField] private Transform slotsContainer;

        private List<InventorySlot> slots;

        #endregion

        #region PROPERTIES

        private Player Player => CharacterManager.Instance.Player;

        #endregion

        #region METHODS

        public override void Initialize()
        {
            base.Initialize();
            slotsContainer.DestroyChildrenImmediate();
            slots = new();

            InitRefreshSlot();
        }

        public override void CleanUp()
        {
            for (int i = slots.Count - 1; i >= 0; i--)
            {
                slots[i].CleanUp();
                ObjectPool.Instance.ReturnToPool(slots[i]);
            }

            base.CleanUp();
        }

        protected override void Refresh()
        {
            base.Refresh();
            RefreshAllSlots();
        }

        protected override void AttachEvents()
        {
            base.AttachEvents();
            if (Player != null)
            {
                Player.EquipmentController.OnItemEquipped += HandleItemEquipped;
                Player.EquipmentController.OnItemCollected += HandleItemCollected;
                Player.EquipmentController.OnItemRemoved += HandleItemRemoved;
                Player.EquipmentController.OnItemsReplaced += HandleItemsReplaced;
            }
        }

        protected override void DetachEvents()
        {
            base.DetachEvents();
            if (Player != null)
            {
                Player.EquipmentController.OnItemEquipped -= HandleItemEquipped;
                Player.EquipmentController.OnItemCollected -= HandleItemCollected;
                Player.EquipmentController.OnItemRemoved -= HandleItemRemoved;
                Player.EquipmentController.OnItemsReplaced -= HandleItemsReplaced;
            }
        }

        private void InitRefreshSlot()
        {
            if (Player == null)
                return;

            foreach (var item in Player.EquipmentController.InventoryModule.ItemsInventory)
                AddSlot(item);

            for (int i = 0; i < Player.EquipmentController.InventoryModule.EmptySlots; i++)
                AddSlot(null);
        }

        private void RefreshAllSlots()
        {
            if (slots == null || slots.Count == 0)
                return;

            for (int i = 0; i < slots.Count; i++)
                slots[i].SetItem(null);

            for (int i = 0; i < Player.EquipmentController.InventoryModule.ItemsInventory.Count; i++)
                slots[i].SetItem(Player.EquipmentController.InventoryModule.ItemsInventory[i]);
        }

        private void AddSlot(Item item)
        {
            InventorySlot slot = ObjectPool.Instance.GetFromPool(slotPrefabId, UIManager.Instance.DefaultUIPoolCategory).GetComponent<InventorySlot>();
            slot.transform.SetParent(slotsContainer);
            slot.SetItem(item);
            slot.Initialize(() => HandleSlotClick(slot));
            slots.Add(slot);
        }

        private void SetItemToFirstEmptySlot(Item item)
        {
            InventorySlot emptySlot = slots.FirstOrDefault(x => x.HasItem == false);
            if (emptySlot == null)
            {
                Debug.LogError("No empty slot for item. This not should happend. Check inventory settings");
                return;
            }

            emptySlot.SetItem(item);
        }

        private void RemoveItemFromInventory(Item item)
        {
            InventorySlot slot = GetSlotByItem(item);
            slot.SetItem(null);

            //Add slot at last place
            slots.Remove(slot);
            slots.Add(slot);
            slot.transform.SetAsLastSibling();
        }

        private InventorySlot GetSlotByItem(Item item)
        {
            return slots.GetElementById(item.Id);
        }

        #region HANDLERS

        private void HandleSlotClick(InventorySlot slot)
        {
            if (slot.HasItem == false)
                return;

            if (slot.IsSelected == false)
            {
                for (int i = 0; i < slots.Count; i++)
                    slots[i].SetSelected(slots[i] == slot);
            }
            else
            {
                Player.EquipmentController.EquipItem(slot.ItemInSlot);
            }
        }

        private void HandleItemsReplaced(Item itemToEquip, Item equippedItem)
        {
            InventorySlot slot = GetSlotByItem(itemToEquip);
            if (slot == null)
            {
                Debug.LogError($"Item wasn't in inventory {itemToEquip.ItemName}");
                SetItemToFirstEmptySlot(equippedItem);
            }
            else
                slot.SetItem(equippedItem);
        }

        private void HandleItemCollected(Item item)
        {
            SetItemToFirstEmptySlot(item);
        }

        private void HandleItemRemoved(Item item)
        {
            RemoveItemFromInventory(item);
        }

        private void HandleItemEquipped(Item item)
        {
            RemoveItemFromInventory(item);
        }

        #endregion

        #endregion
    }
}
