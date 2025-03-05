using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    [Serializable]
    public class PoolObject
    {
        #region VARIABLES

        [SerializeField] private string name;
        [SerializeField] private string category;
        [SerializeField] private GameObject prefab;
        [SerializeField] private Dictionary<Type, Component> components;
        [SerializeField] private bool taken;

        #endregion

        #region PROPERTIES

        public string Name => name;
        public string Category => category;
        public GameObject Prefab => prefab;
        public bool Taken => taken;

        #endregion

        #region CONSTRUCTORS

        public PoolObject(string name, string category, GameObject prefab)
        {
            this.name = name;
            this.category = category;
            this.prefab = prefab;

            components = new Dictionary<Type, Component>();
            Component[] componentsTmp = prefab.GetComponents(typeof(Component));
            foreach (var compTmp in componentsTmp)
            {
                if (compTmp is IPoolable poolableComp)
                    poolableComp.AssignPoolable(this);
                components[compTmp.GetType()] = compTmp;
            }
        }

        #endregion

        #region METHODS

        public Component GetComponent(Type type)
        {
            Component component = null;
            components.TryGetValue(type, out component);
            if (!component)
                foreach (var comp in components)
                {
                    if (type.IsAssignableFrom(comp.Key))
                        return comp.Value;
                }
            return component;
        }

        public void SetTaken(bool status)
        {
            taken = status;
        }

        public T GetComponent<T>() where T : Component
        {
            return GetComponent(typeof(T)) as T;
        }

        #endregion
    }
}
