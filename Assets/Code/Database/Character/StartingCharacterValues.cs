using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Character.Data
{
    //TODO Create database with modifiable categories and take it by ids
    [System.Serializable]
    public class StartingCharacterValues
    {
        #region VARIABLES

        [SerializeField] private float health;
        [SerializeField] private float damage;
        [SerializeField] private float defence;
        [SerializeField] private float critChance;
        [SerializeField] private float lifeSteal;
        [SerializeField] private float attackSpeed;
        [SerializeField] private float movementSpeed;
        [SerializeField] private float luck;

        #endregion

        #region PROPERTIES

        public float Health => health;
        public float Damage => damage;
        public float Defence => defence;
        public float CritChance => critChance;
        public float LifeSteal => lifeSteal;
        public float AttackSpeed => attackSpeed;
        public float MovementSpeed => movementSpeed;
        public float Luck => luck;

        #endregion
    }
}
