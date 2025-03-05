using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace ObjectPooling
{
    [Serializable]
    public class PoolInstanceData : IIdEqualable
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private int id = Guid.NewGuid().GetHashCode();
        [SerializeField] private string name;
        [SerializeField] private GameObject poolObject;
        [SerializeField] private int size;

        #endregion

        #region PROPERTIES

        public int Id => id;
        public string Name => name;
        public GameObject PoolObject => poolObject;
        public int Size => size;

        #endregion

        #region CONSTRUCTOES

        public PoolInstanceData() { }
        public PoolInstanceData(string name, GameObject poolObject, int size)
        {
            this.name = name;
            this.poolObject = poolObject;
            this.size = size;
        }

        #endregion

        #region METHODS

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}
