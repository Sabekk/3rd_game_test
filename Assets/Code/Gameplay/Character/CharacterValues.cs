using Database.Character.Data;
using Gameplay.Values;
using UnityEngine;

namespace Gameplay.Character.Values
{
    [System.Serializable]
    public class CharacterValues
    {
        #region VARIABLES

        //TODO Create database and make this as category

        [SerializeField] private ModifiableValue currentHealth;
        [SerializeField] private ModifiableValue health;
        [SerializeField] private ModifiableValue damage;
        [SerializeField] private ModifiableValue defence;
        [SerializeField] private ModifiableValue critChance;
        [SerializeField] private ModifiableValue lifeSteal;
        [SerializeField] private ModifiableValue attackSpeed;
        [SerializeField] private ModifiableValue movementSpeed;
        [SerializeField] private ModifiableValue luck;

        #endregion

        #region PROPERTIES
        public ModifiableValue CurrentHealth => currentHealth;
        public ModifiableValue Damage => damage;
        public ModifiableValue Health => health;
        public ModifiableValue Defence => defence;
        public ModifiableValue LifeSteal => lifeSteal;
        public ModifiableValue CritChance => critChance;
        public ModifiableValue AttackSpeed => attackSpeed;
        public ModifiableValue MovementSpeed => movementSpeed;
        public ModifiableValue Luck => luck;

        #endregion

        #region METHODS

        public void SetStartingValues(StartingCharacterValues startingValues)
        {
            health = new(startingValues.Health);
            damage = new(startingValues.Damage);
            defence = new(startingValues.Defence);
            critChance = new(startingValues.CritChance, ValueType.PERCENTAGE);
            lifeSteal = new(startingValues.LifeSteal, ValueType.PERCENTAGE);
            attackSpeed = new(startingValues.AttackSpeed, ValueType.PERCENTAGE);
            movementSpeed = new(startingValues.MovementSpeed, ValueType.PERCENTAGE);
            luck = new(startingValues.Luck, ValueType.PERCENTAGE);
        }

        #endregion
    }
}