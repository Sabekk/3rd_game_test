using Gameplay.Items;
using ObjectPooling;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Window.Inventory
{
    public class InventorySlot : MonoBehaviour, IIdEqualable, IPoolable
    {
        #region VARIABLES

        [SerializeField] private Image frame;
        [SerializeField] private Image itemIcon;

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
            {
                itemIcon.gameObject.SetActiveOptimize(true);
                itemIcon.sprite = ItemInSlot.Data.Icon;
            }
            else
            {
                itemIcon.sprite = null;
                itemIcon.gameObject.SetActiveOptimize(false);
            }
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