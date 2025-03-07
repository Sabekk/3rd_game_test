using Gameplay.Items;
using ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Window.Inventory
{
    [RequireComponent(typeof(Button))]
    public abstract class SlotBase : MonoBehaviour, IIdEqualable, IPoolable
    {
        #region ACTIONS

        private Action OnClickAction;

        #endregion

        #region VARIABLES

        [SerializeField] protected Button button;
        [SerializeField] protected GameObject selectionFrame;
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
        public bool HasItem => ItemInSlot != null;
        public bool IsSelected { get; private set; }

        #endregion

        #region METHODS

        public virtual void Initialize(Action onClickAction)
        {
            OnClickAction = onClickAction;
            button.onClick.AddListener(HandleClickButton);
        }

        public virtual void CleanUp()
        {
            button.onClick.RemoveListener(HandleClickButton);
        }

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

        public void SetSelected(bool state)
        {
            selectionFrame.SetActiveOptimize(state);
            IsSelected = state;
        }

        protected void RefreshItemInSlot()
        {
            SetIcon();
            SetFrame();
        }

        protected void SetFrame()
        {
            Sprite frameToSet = defaultFrame;

            if (HasItem)
            {
                SpriteAndIdPair spritePair = rarityFrames.GetElementById(ItemInSlot.Rarity);
                if (spritePair != null)
                    frameToSet = spritePair.Sprite;
            }

            frame.sprite = frameToSet;
        }

        protected virtual void SetIcon()
        {
            if (HasItem)
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

        #region HANDLERS

        private void HandleClickButton()
        {
            OnClickAction?.Invoke();
        }

        #endregion

        #endregion
    }
}