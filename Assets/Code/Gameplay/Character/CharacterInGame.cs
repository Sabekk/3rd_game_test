using Gameplay.Character.Items;
using Gameplay.Character.Values;
using Gameplay.Targeting;
using ObjectPooling;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public class CharacterInGame : DamageTarget, IPoolable
    {
        #region ACTION

        public event Action OnKill;

        #endregion

        #region VARIABLES

        [SerializeField] private Animator aniamtor;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private List<ItemSocket> itemSockets;

        #endregion

        #region PROPERTIES

        public CharacterBase Character { get; set; }
        public Animator Aniamtor => aniamtor;
        public CharacterController CharacterController => characterController;
        public List<ItemSocket> ItemSockets => itemSockets;

        public override float Health
        {
            get => CharacterValues.CurrentHealth.CurrentValue;
            set => CharacterValues.CurrentHealth.SetRawValue(value);
        }

        public override float MaxHealth => CharacterValues.Health.CurrentValue;
        public override bool IsKilled { get; set; }
        public PoolObject Poolable { get; set; }
        private CharacterValues CharacterValues => Character.ValuesController.CharacterValues;

        #endregion

        #region METHODS

        public override void Kill()
        {
            base.Kill();
            OnKill?.Invoke();
        }

        public void AssignPoolable(PoolObject poolable)
        {
            Poolable = poolable;
        }


        [Button]
        private void CollectAllSocketsForItems()
        {
            if (itemSockets == null)
                itemSockets = new();
            itemSockets.Clear();

            var sockets = GetComponentsInChildren<ItemSocket>();
            itemSockets.AddRange(sockets);
        }

        #endregion
    }
}
