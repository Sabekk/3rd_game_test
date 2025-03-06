using Gameplay.Character;
using Gameplay.Items;
using ObjectPooling;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
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
            base.CleanUp();
            for (int i = slots.Count - 1; i >= 0; i--)
            {
                ObjectPool.Instance.ReturnToPool(slots[i]);
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

        private void AddSlot(Item item)
        {
            InventorySlot slot = ObjectPool.Instance.GetFromPool(slotPrefabId, UIManager.Instance.DefaultUIPoolCategory).GetComponent<InventorySlot>();
            slot.transform.SetParent(slotsContainer);
            slot.SetItem(item);
            slots.Add(slot);
        }

        #endregion
    }
}
