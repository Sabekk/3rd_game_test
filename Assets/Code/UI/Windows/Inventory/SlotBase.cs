using Gameplay.Items;
using ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Window.Inventory
{
    public class SlotBase : MonoBehaviour, IIdEqualable, IPoolable
    {
        #region VARIABLES

        [SerializeField] protected Image frame;
        [SerializeField] protected Image itemIcon;
        [SerializeField] protected Sprite defaultFrame;

        [SerializeField] private List<SpriteAndIdPair> rarityFrames;

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
            RefreshItemInSlot();
        }

        public void RemoveItem()
        {
            itemInSlot = null;
            RefreshItemInSlot();
        }

        protected void RefreshItemInSlot()
        {
            SetIcon();
            SetFrame();
        }

        protected void SetFrame()
        {
            Sprite frameToSet = defaultFrame;

            if (ItemInSlot != null)
            {
                SpriteAndIdPair spritePair = rarityFrames.GetElementById(ItemInSlot.Rarity);
                if (spritePair != null)
                    frameToSet = spritePair.Sprite;
            }

            frame.sprite = frameToSet;
        }

        protected virtual void SetIcon()
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