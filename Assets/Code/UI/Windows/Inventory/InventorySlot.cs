using Gameplay.Items;
using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Window.Inventory
{
    public class InventorySlot : MonoBehaviour, IIdEqualable, IPoolable
    {
        #region VARIABLES

        [SerializeField] private Image icon;
        [SerializeField] private Sprite emptyIcon;

        private Item itemInSlot;
        private int itemId;

        #endregion

        #region PROPERTIES

        public int Id => itemId;
        public Item ItemInSlot => itemInSlot;
        public PoolObject Poolable { get; set; }

        #endregion

        #region METHODS

        public void SetItem(Item item)
        {
            itemInSlot = item;
            itemId = item != null ? item.Id : -1;
            SetIcon();
        }

        public void RemoveItem()
        {
            itemInSlot = null;
            SetIcon();
        }

        private void SetIcon()
        {
            if (ItemInSlot != null)
                icon.sprite = ItemInSlot.Icon;
            else
                icon.sprite = emptyIcon;
        }

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        public void AssignPoolable(PoolObject poolable)
        {
            Poolable = poolable;
        }

        #endregion
    }
}