using Gameplay.Character.Items;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character
{
    public class CharacterInGame : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private Animator aniamtor;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private List<ItemSocket> itemSockets;

        #endregion

        #region PROPERTIES

        public Animator Aniamtor => aniamtor;
        public CharacterController CharacterController => characterController;
        public List<ItemSocket> ItemSockets => itemSockets;

        #endregion

        #region METHODS

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
