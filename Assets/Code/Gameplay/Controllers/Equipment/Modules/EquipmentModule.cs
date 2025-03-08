using Gameplay.Controller.Module;
using Gameplay.Items;
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

        internal bool EquipItem(Item item)
        {
            if (EquippedItems.TryAdd(item.Category, item))
                return true;
            return false;
        }

        internal bool UneqipItem(Item item)
        {
            if (EquippedItems.ContainsKey(item.Category))
            {
                EquippedItems.Remove(item.Category);
                return true;
            }

            return false;
        }


        #endregion
    }
}