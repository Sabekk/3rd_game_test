using Gameplay.Controller.Module;
using Gameplay.Items;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Equipment
{
    public class InventoryModule : ControllerModuleBase
    {
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

        internal bool AddItem(Item item, bool checkAvailability = false)
        {
            if (checkAvailability && CanAddItem() == false)
                return false;

            ItemsInventory.Add(item);
            return true;
        }

        internal bool RemoveItem(Item item, bool checkCointains = true)
        {
            if (checkCointains && ItemsInventory.ContainsId(item.Id) == false)
                return false;

            ItemsInventory.Remove(item);
            return true;
        }


        #endregion
    }
}