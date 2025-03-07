using Database;
using Database.Items;
using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    public class Item : IIdEqualable
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private int id;
        [SerializeField] private string itemName;
        [SerializeField] private ItemCategory category;
        [SerializeField] private ItemType itemType;
        [SerializeField] private int rarity;
        [SerializeField] private int damage;
        [SerializeField] private int healthPoints;
        [SerializeField] private int defense;
        [SerializeField] private int lifeSteal;
        [SerializeField] private int criticalStrikeChance;
        [SerializeField] private int attackSpeed;
        [SerializeField] private int movementSpeed;
        [SerializeField] private int luck;
        [SerializeField, HideInInspector] private int dataId;

        private ItemData data;

        #endregion

        #region PROPERTIES

        public string ItemName => itemName;
        public ItemCategory Category => category;
        public ItemType ItemType => itemType;
        public int Id => id;
        public int Rarity => rarity;
        public int Damage => damage;
        public int HealthPoints => healthPoints;
        public int Defense => defense;
        public int LifeSteal => lifeSteal;
        public int CriticalStrikeChance => criticalStrikeChance;
        public int AttackSpeed => attackSpeed;
        public int MovementSpeed => movementSpeed;
        public int Luck => luck;
        public ItemData Data
        {
            get
            {
                if (data == null)
                    CacheDataById();
                return data;
            }
        }

        #endregion

        #region CONSTRUCTORS

        public Item() { }
        public Item(JObject jObject)
        {
            id = Guid.NewGuid().GetHashCode();
            string itemName = jObject.Value<string>("Name");
            string categoryName = jObject.Value<string>("Category");
            string typeOfItem = itemName.Replace(categoryName, "");

            if (Enum.TryParse(categoryName, true, out ItemCategory category))
                this.category = category;
            else
            {
                Debug.LogError($"Missing item category [{categoryName}]");
                this.category = ItemCategory.Armor;
            }

            if (Enum.TryParse(typeOfItem, true, out ItemType itemType))
                this.itemType = itemType;
            else
            {
                Debug.LogError($"Missing item type [{typeOfItem}]");
                this.itemType = ItemType.Death;
            }

            this.itemName = itemName;
            rarity = jObject.Value<int>("Rarity");
            damage = jObject.Value<int>("Damage");
            healthPoints = jObject.Value<int>("HealthPoints");
            defense = jObject.Value<int>("Defense");
            lifeSteal = jObject.Value<int>("LifeSteal");
            criticalStrikeChance = jObject.Value<int>("CriticalStrikeChance");
            attackSpeed = jObject.Value<int>("AttackSpeed");
            movementSpeed = jObject.Value<int>("MovementSpeed");
            luck = jObject.Value<int>("Luck");
            CacheDataByItemType();
        }

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        private void CacheDataById()
        {
            data = MainDatabases.Instance.ItemsDatabase.GetItemData(dataId, category);
        }
        private void CacheDataByItemType()
        {
            data = MainDatabases.Instance.ItemsDatabase.GetItemData(itemType, category);
        }

        #endregion

        #region METHODS

        #endregion
    }
}