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
        [SerializeField] private string characterName;
        [SerializeField, ValueDropdown(ObjectPoolDatabase.GET_POOL_CHARACTERS_IN_GAME_METHOD)] private int characterInGamePoolId;

        #endregion

        #region PROPERTIES

        public int Id => id;
        public int CharacterInGamePoolId => characterInGamePoolId;
        public string CharacterName => characterName;

        #endregion

        #region METHODS

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}
