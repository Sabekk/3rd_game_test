using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    [Serializable]
    public class PoolInstancesGroupData
    {
        #region VARIABLES

        [SerializeField] private string groupName;
        [SerializeField] private List<PoolInstanceData> instances;

        #endregion

        #region CONSTRUCTORS

        public PoolInstancesGroupData() { }
        public PoolInstancesGroupData(string groupName)
        {
            this.groupName = groupName;
        }

        #endregion

        #region PROPERTIES

        public string GroupName => groupName;
        public List<PoolInstanceData> Instances
        {
            get
            {
                if (instances == null)
                    instances = new();
                return instances;
            }
            set { instances = value; }
        }

        #endregion

        #region METHODS

        public void AddInstance(PoolInstanceData instance)
        {
            Instances.Add(instance);
        }

        public void RemoveInstance(PoolInstanceData instance)
        {
            Instances.Remove(instance);
        }

        public PoolInstanceData FindInstanceData(string instanceDataName)
        {
            foreach (var instanceData in Instances)
            {
                if (instanceData.Name == instanceDataName)
                    return instanceData;
            }

            return null;
        }

        public PoolInstanceData FindInstanceData(int instanceDataId)
        {
            foreach (var instanceData in Instances)
            {
                if (instanceData.IdEquals(instanceDataId))
                    return instanceData;
            }

            return null;
        }

        #endregion
    }
}