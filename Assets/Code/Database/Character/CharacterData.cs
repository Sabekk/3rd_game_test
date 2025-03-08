using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Character.Data
{
    [System.Serializable]
    [CreateAssetMenu(menuName = "Data/Character/CharacterData", fileName = "CharacterData")]
    public class CharacterData : ScriptableObject, IIdEqualable
    {
        #region VARIABLES

        //Starting values?
        [SerializeField, ReadOnly] private int id = Guid.NewGuid().GetHashCode();

        [SerializeField, FoldoutGroup("Character settings")] private string characterName;
        [SerializeField, FoldoutGroup("Character settings"), ValueDropdown(ObjectPoolDatabase.GET_POOL_CHARACTERS_IN_GAME_METHOD)] private int characterInGamePoolId;

        [SerializeField, FoldoutGroup("Starting settings")] private int maxInventorySlots;
        [SerializeField, FoldoutGroup("Starting settings")] private StartingCharacterValues startingValues;

        #endregion

        #region PROPERTIES

        public int Id => id;
        public int CharacterInGamePoolId => characterInGamePoolId;
        public int MaxInventorySlots => maxInventorySlots;
        public string CharacterName => characterName;
        public StartingCharacterValues StartingValues => startingValues;

        #endregion

        #region METHODS

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}
