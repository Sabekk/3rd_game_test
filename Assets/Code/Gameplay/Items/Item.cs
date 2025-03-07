using Database;
using Newtonsoft.Json.Linq;
using Sirenix.OdinInspector;
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

        private Sprite icon;

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
        public Sprite Icon
        {
            get
            {
                if (icon == null)
                    CacheIcon();
                return icon;
            }
        }

        #endregion

        #region CONSTRUCTORS

        public Item() { }
        public Item(JObject jObject)
        {
            string itemName = jObject.Value<string>("Name");
            string categoryName = jObject.Value<string>("Category");
            string typeOfItem = itemName.Replace(categoryName, "");

            if (System.Enum.TryParse(categoryName, true, out ItemCategory category))
                this.category = category;
            else
            {
                Debug.LogError($"Missing item category [{categoryName}]");
                this.category = ItemCategory.Armor;
            }

            if (System.Enum.TryParse(typeOfItem, true, out ItemType itemType))
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
            CacheIcon();
        }

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        private void CacheIcon()
        {
            icon = MainDatabases.Instance.ItemsDatabase.TryGetItemInCategoryIcon(ItemType, Category);
        }

        #endregion

        #region METHODS

        #endregion
    }
}