using Gameplay.Character;
using Gameplay.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Values
{
    [Serializable]
    public class ValuesController : CharacterControllerBase
    {
        #region VARIABLES

        [SerializeField] private CharacterValues characterValues;

        #endregion

        #region PROPERTIES

        public CharacterValues CharacterValues => characterValues;

        #endregion

        #region METHODS

        public override void Initialize(CharacterBase character)
        {
            base.Initialize(character);
            characterValues = new();
            characterValues.SetStartingValues(character.Data.StartingValues);
        }

        public override void AttachEvents()
        {
            base.AttachEvents();
            Character.EquipmentController.OnItemEquipped += HandleItemEquipped;
            Character.EquipmentController.OnItemUnequipped += HandleItemUnequipped;
            Character.EquipmentController.OnItemsReplaced += HandleItemsReplaced;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();
            Character.EquipmentController.OnItemEquipped -= HandleItemEquipped;
            Character.EquipmentController.OnItemUnequipped -= HandleItemUnequipped;
            Character.EquipmentController.OnItemsReplaced -= HandleItemsReplaced;
        }

        private void AddItemValues(Item item)
        {
            characterValues.Damage.AddNewComponent(item.Damage);
            characterValues.Health.AddNewComponent(item.HealthPoints);
            characterValues.Defence.AddNewComponent(item.Defence);
            characterValues.LifeSteal.AddNewComponent(item.LifeSteal);
            characterValues.CritChance.AddNewComponent(item.CritChance);
            characterValues.AttackSpeed.AddNewComponent(item.AttackSpeed);
            characterValues.MovementSpeed.AddNewComponent(item.MovementSpeed);
            characterValues.Luck.AddNewComponent(item.Luck);
        }

        private void RemoveItemValues(Item item)
        {
            characterValues.Damage.RemoveComponent(item.Damage);
            characterValues.Health.RemoveComponent(item.HealthPoints);
            characterValues.Defence.RemoveComponent(item.Defence);
            characterValues.LifeSteal.RemoveComponent(item.LifeSteal);
            characterValues.CritChance.RemoveComponent(item.CritChance);
            characterValues.AttackSpeed.RemoveComponent(item.AttackSpeed);
            characterValues.MovementSpeed.RemoveComponent(item.MovementSpeed);
            characterValues.Luck.RemoveComponent(item.Luck);
        }

        #region HANDLERS

        private void HandleItemsReplaced(Item newItem, Item oldItem)
        {
            RemoveItemValues(oldItem);
            AddItemValues(newItem);
        }

        private void HandleItemEquipped(Item item)
        {
            AddItemValues(item);
        }

        private void HandleItemUnequipped(Item item)
        {
            RemoveItemValues(item);
        }

        #endregion

        #endregion
    }
}