using Database;
using Database.Character.Data;
using Gameplay.Character.Values;
using Gameplay.Equipment;
using Gameplay.Targeting;
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

        [SerializeField, FoldoutGroup("Controllers")] private ValuesController valuesController;
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
        public ValuesController ValuesController => valuesController;

        public bool CanAttack => true;

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
            SetControllers();
            InitializeControllers();

            isInitialzied = true;
        }

        public void CleanUp()
        {
            if (CharacterInGame)
            {
                CharacterInGame.OnKill -= HandleCharacterKill;
                ObjectPool.Instance.ReturnToPool(CharacterInGame);
            }
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

            characterInGame = ObjectPool.Instance.GetFromPool(Data.CharacterInGamePoolId, -1).GetComponent<CharacterInGame>();
            if (characterInGame != null)
            {
                characterInGame.OnKill += HandleCharacterKill;
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

            valuesController = new();
            equipmentController = new();

            controllers.Add(valuesController);
            controllers.Add(equipmentController);
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


        #region HANDLERS

        private void HandleCharacterKill()
        {
            if (CharacterInGame)
                CharacterInGame.OnKill -= HandleCharacterKill;
            Debug.Log("DEATH");

            CharacterManager.Instance.RemoveCharacter(this);
        }

        #endregion

        #endregion
    }
}
