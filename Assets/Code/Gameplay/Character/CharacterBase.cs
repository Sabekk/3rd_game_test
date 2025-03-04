using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public class CharacterBase
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private int idOfCharacterInGame;
        [SerializeField, HideInInspector] protected List<CharacterController> controllers;
        [SerializeField, HideInInspector] private bool isInitialzied;

        #endregion

        #region PROPERTIES

        #endregion

        protected virtual void OnUpdate()
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
        #region METHODS

        protected virtual void SetControllers()
        {
            controllers = new();

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
