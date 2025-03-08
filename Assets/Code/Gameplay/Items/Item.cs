using Database;
using Database.Items;
using Gameplay.Values;
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
        [SerializeField] private ModifiableValue healthPoints;
        [SerializeField] private ModifiableValue damage;
        [SerializeField] private ModifiableValue defence;
        [SerializeField] private ModifiableValue critChance;
        [SerializeField] private ModifiableValue lifeSteal;
        [SerializeField] private ModifiableValue attackSpeed;
        [SerializeField] private ModifiableValue movementSpeed;
        [SerializeField] private ModifiableValue luck;

        [SerializeField, HideInInspector] private int dataId;

        private ItemData data;

        #endregion

        #region PROPERTIES

        public string ItemName => itemName;
        public ItemCategory Category => category;
        public ItemType ItemType => itemType;
        public int Id => id;
        public int Rarity => rarity;
        public ModifiableValue Damage => damage;
        public ModifiableValue HealthPoints => healthPoints;
        public ModifiableValue Defence => defence;
        public ModifiableValue LifeSteal => lifeSteal;
        public ModifiableValue CritChance => critChance;
        public ModifiableValue AttackSpeed => attackSpeed;
        public ModifiableValue MovementSpeed => movementSpeed;
        public ModifiableValue Luck => luck;
        public ItemData Data
        {
            get
            {
                if (data == null)
                    CacheDataById();
                return data;
            }
        }

        public List<ItemVisualizationBase> Visualizations { get; set; }

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
            damage = new(jObject.Value<int>("Damage"));
            healthPoints = new(jObject.Value<int>("HealthPoints"));
            defence = new(jObject.Value<int>("Defense"));
            lifeSteal = new(jObject.Value<int>("LifeSteal"));
            critChance = new(jObject.Value<int>("CriticalStrikeChance"));
            attackSpeed = new(jObject.Value<int>("AttackSpeed"));
            movementSpeed = new(jObject.Value<int>("MovementSpeed"));
            luck = new(jObject.Value<int>("Luck"));

            CacheDataByItemType();
            Visualizations = new();
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