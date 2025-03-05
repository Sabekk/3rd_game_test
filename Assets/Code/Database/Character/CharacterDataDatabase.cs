using Database.Character.Data;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Database.Character
{
    [CreateAssetMenu(menuName = "Database/CharacterDataDatabase", fileName = "CharacterDataDatabase")]

    public class CharacterDataDatabase : ScriptableObject
    {
        #region VARIABLES

        public const string GET_DATA_METHOD = "@" + nameof(CharacterDataDatabase) + "." + nameof(GetDatasNames) + "()";

        [SerializeReference] private CharacterData defaultData;
        [SerializeField] private List<CharacterData> charactersData;

        #endregion

        #region PROPERTIES

        public List<CharacterData> CharactersDatas => charactersData;

        #endregion

        #region METHODS

        public static IEnumerable GetDatasNames()
        {
            ValueDropdownList<int> values = new();
            foreach (CharacterData characterData in MainDatabases.Instance.CharacterDataDatabase.CharactersDatas)
                values.Add(characterData.CharacterName, characterData.Id);

            return values;
        }

        public CharacterData GetData(int dataId)
        {
            CharacterData data = CharactersDatas.Find(x => x.IdEquals(dataId));
            if (data == null)
                data = defaultData;

            return data;
        }

        #endregion
    }
}
