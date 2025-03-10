using Database;
using Database.Character.Data;
using Gameplay.Character.Attacking;
using Gameplay.Character.Movement;
using Gameplay.Character.Values;
using Gameplay.Equipment;
using ObjectPooling;
using Sirenix.OdinInspector;
using System;
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

        [SerializeField, FoldoutGroup("Controllers")] protected ValuesController valuesController;
        [SerializeField, FoldoutGroup("Controllers")] protected CharacterMovementController movementController;
        [SerializeField, FoldoutGroup("Controllers")] protected EquipmentController equipmentController;
        [SerializeField, FoldoutGroup("Controllers")] protected AttackingController attackingController;

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
        public AttackingController AttackingController => attackingController;
        public CharacterMovementController MovementController => movementController;

        #endregion

        #region CONSTRUCTORS

        public CharacterBase()
        {
            CreateControllers();
        }

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

        public void AttachEvents()
        {
            AttachControllers();
        }

        public void DetachEvents()
        {
            DetachControllers();
        }


        public void CleanUp()
        {
            CleanUpControllers();
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
                characterInGame.Initialize(this);
                characterInGame.OnKill += HandleCharacterKill;
                characterInGame.transform.SetParent(parent);
                OnCharacterInGameCreated?.Invoke();
                return true;
            }
            else
                return false;
        }

        protected virtual void CreateControllers()
        {
            valuesController = new ValuesController();
            equipmentController = new EquipmentController();
            attackingController = new AttackingController();
            movementController = new CharacterMovementController();
        }

        protected virtual void SetControllers()
        {
            controllers = new();

            controllers.Add(valuesController);
            controllers.Add(equipmentController);
            controllers.Add(movementController);
            controllers.Add(attackingController);
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

        protected void AttachControllers()
        {
            controllers.ForEach(m => m.AttachEvents());
        }

        protected void DetachControllers()
        {
            controllers.ForEach(m => m.DetachEvents());
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
