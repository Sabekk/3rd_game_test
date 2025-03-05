using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

namespace ObjectPooling
{
    [Serializable]
    public class PoolCategoryData : IIdEqualable
    {
        #region VARIABLES

        [SerializeField, ReadOnly] private int id = Guid.NewGuid().GetHashCode();
        [SerializeField] private string categoryName;
        [SerializeField] private List<PoolInstancesGroupData> groups;

        #endregion

        #region PROPERTIES

        public int Id => id;
        public string CategoryName => categoryName;
        public List<PoolInstanceData> Instances
        {
            get
            {
                List<PoolInstanceData> instances = new();
                for (int i = 0; i < groups.Count; i++)
                    instances.AddRange(groups[i].Instances);

                return instances;
            }
        }
        public List<PoolInstancesGroupData> Groups => groups;


        #endregion

        #region CONSTRUCTORS

        public PoolCategoryData() { }
        public PoolCategoryData(string name)
        {
            categoryName = name;
        }

        #endregion

        #region METHODS

        public void AddInstance(PoolInstanceData instance)
        {
            //instances.Add(instance);

            PoolInstancesGroupData group = FindGroup(instance.Name);
            if (group == null)
            {
                group = new(GetGroupName(instance.Name));
                Groups.Add(group);
            }

            group.AddInstance(instance);
        }

        public void RemoveInstance(PoolInstanceData instance)
        {
            //instances.Remove(instance);

            PoolInstancesGroupData group = FindGroup(instance.Name);
            if (group == null)
                return;

            group.RemoveInstance(instance);
            if (group.Instances.Count <= 0)
                Groups.Remove(group);
        }

        public PoolInstanceData FindInstanceData(string instanceDataName)
        {
            PoolInstancesGroupData group = FindGroup(GetGroupName(instanceDataName));
            if (group != null)
                return group.FindInstanceData(instanceDataName);
            return null;
        }

        public PoolInstanceData FindInstanceData(int instanceDataId)
        {
            PoolInstancesGroupData group = FindGroup(instanceDataId, out PoolInstanceData instanceData);
            if (group != null)
                return instanceData;

            return null;
        }

        public PoolInstancesGroupData FindGroup(string instanceDataName)
        {
            string groupName = GetGroupName(instanceDataName);
            return Groups.Find(x => x.GroupName == groupName);
        }

        public PoolInstancesGroupData FindGroup(int instanceDataId, out PoolInstanceData instanceData)
        {
            instanceData = null;

            foreach (var group in Groups)
            {
                instanceData = group.FindInstanceData(instanceDataId);
                if (instanceData != null)
                    return group;
            }

            return null;
        }

        public string GetGroupName(string instanceDataName)
        {
            int lastIndexOfNumber = instanceDataName.LastIndexOf("_");
            if (lastIndexOfNumber < 0)
                return instanceDataName;
            else
            {
                string groupName = instanceDataName.Remove(lastIndexOfNumber, instanceDataName.Length - lastIndexOfNumber);
                return groupName;
            }
        }

        public bool IdEquals(int id)
        {
            return Id == id;
        }

        #endregion
    }
}
