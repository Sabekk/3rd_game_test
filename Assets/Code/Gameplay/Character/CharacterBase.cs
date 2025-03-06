using Database;
using Database.Character.Data;
using Gameplay.Equipment;
using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public class CharacterBase
    {
        #region ACTION

        public event Action OnCharacterInGameCreated;

        #endregion

        #region VARIABLES

        [SerializeField, ReadOnly] private int idOfCharacterInGame;
        [SerializeField] protected List<CharacterControllerBase> controllers;
        [SerializeField] private bool isInitialzied;
        [SerializeField] private int dataId;

        [SerializeField, FoldoutGroup("Controllers")] private EquipmentController equipmentController;

        private CharacterInGame characterInGame;
        private CharacterData data;

        #endregion

        #region PROPERTIES

        public CharacterData Data
        {
            get
            {
                if (data == null)
                    data = MainDatabases.Instance.CharacterDataDatabase.GetData(dataId);
                return data;
            }
        }

        public CharacterInGame CharacterInGame => characterInGame;
        public EquipmentController EquipmentController => equipmentController;

        #endregion

        #region CONSTRUCTORS

        public CharacterBase() { }

        #endregion

        #region METHODS

        public virtual void OnUpdate()
        {
            if (!isInitialzied)
                return;

            UpdateControllers();
        }

        public void Initialize()
        {
            //TODO Add values

            SetControllers();
            InitializeControllers();

            isInitialzied = true;
        }

        public void SetData(CharacterData data)
        {
            dataId = data.Id;
            this.data = data;
        }

        public bool TryCreateVisualization(Transform parent = null)
        {
            if (Data == null)
                return false;

            if (characterInGame != null)
                return true;

            characterInGame = ObjectPool.Instance.GetFromPool(Data.CharacterInGamePoolId).GetComponent<CharacterInGame>();
            if (characterInGame != null)
            {
                characterInGame.transform.SetParent(parent);
                OnCharacterInGameCreated?.Invoke();
                return true;
            }
            else
                return false;
        }

        protected virtual void SetControllers()
        {
            controllers = new();
            controllers.Add(equipmentController = new());
        }

        protected void InitializeControllers()
        {
            controllers.ForEach(m => m.Initialize(this));
        }

        protected void UpdateControllers()
        {
            controllers?.ForEach(m => m.OnUpdate());
        }

        protected void CleanUpControllers()
        {
            controllers.ForEach(m => m.CleanUp());
        }

        #endregion
    }
}
