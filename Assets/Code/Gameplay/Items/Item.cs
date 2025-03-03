using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Items
{
    public class Item : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private string itemName;
        [SerializeField] private ItemCategory category;
        [SerializeField] private int rarity;
        [SerializeField] private int damage;
        [SerializeField] private int healthPoints;
        [SerializeField] private int defense;
        [SerializeField] private int lifeSteal;
        [SerializeField] private int criticalStrikeChance;
        [SerializeField] private int attackSpeed;
        [SerializeField] private int movementSpeed;
        [SerializeField] private int luck;

        #endregion

        #region PROPERTIES

        public string ItemName => itemName;
        public ItemCategory Category => category;
        public int Rarity => rarity;
        public int Damage => damage;
        public int HealthPoints => healthPoints;
        public int Defense => defense;
        public int LifeSteal => lifeSteal;
        public int CriticalStrikeChance => criticalStrikeChance;
        public int AttackSpeed => attackSpeed;
        public int MovementSpeed => movementSpeed;
        public int Luck => luck;

        #endregion

        #region CONSTRUCTORS

        public Item() { }
        public Item(JObject jObject)
        {
            itemName = jObject.Value<string>("Name");
            category = (ItemCategory)System.Enum.Parse(typeof(ItemCategory), jObject.Value<string>("Category"));
            rarity = jObject.Value<int>("Rarity");
            damage = jObject.Value<int>("Damage");
            healthPoints = jObject.Value<int>("HealthPoints");
            defense = jObject.Value<int>("Defense");
            lifeSteal = jObject.Value<int>("LifeSteal");
            criticalStrikeChance = jObject.Value<int>("CriticalStrikeChance");
            attackSpeed = jObject.Value<int>("AttackSpeed");
            movementSpeed = jObject.Value<int>("MovementSpeed");
            luck = jObject.Value<int>("Luck");
        }

        #endregion

        #region METHODS

        #endregion
    }
}