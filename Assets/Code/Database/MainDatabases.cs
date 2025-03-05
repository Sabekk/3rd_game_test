using Database.Character;
using ObjectPooling;
using UnityEngine;

namespace Database
{
    [CreateAssetMenu(menuName = "Database/MainDatabases", fileName = "MainDatabases")]
    public class MainDatabases : ScriptableSingleton<MainDatabases>
    {
        #region VARIABLES

        [SerializeField] private ObjectPoolDatabase objectPoolDatabase;
        [SerializeField] private CharacterDataDatabase characterDataDatabase;

        #endregion

        #region PROPERTIES

        public new static MainDatabases Instance => GetInstance("Singletons/MainDatabases");

        public ObjectPoolDatabase ObjectPoolDatabase => objectPoolDatabase;
        public CharacterDataDatabase CharacterDataDatabase => characterDataDatabase;

        #endregion

        #region METHODS

        #endregion
    }
}