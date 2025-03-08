using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Character.Items
{
    public class ItemSocket : MonoBehaviour
    {
        #region VARIABLES

        [SerializeField] private ItemCategory categoryItem;

        #endregion

        #region PROPERTIES

        public ItemCategory CategoryItem => categoryItem;

        #endregion

        #region METHODS

        #endregion
    }
}