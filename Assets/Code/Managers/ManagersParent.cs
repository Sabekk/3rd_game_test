using Gameplay.Cameras;
using Gameplay.Character;
using Gameplay.GameStates;
using Gameplay.Inputs;
using Gameplay.Items;
using Gameplay.Timing;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagersParent : MonoSingleton<ManagersParent>
{
    #region ACTION

    public event Action OnManagersInitialized;

    #endregion

    #region VARIABLES

    [SerializeField] protected List<IGameplayManager> managers = new();

    #endregion

    #region PROPERTIES

    public bool Initialized { get; set; }

    #endregion

    #region UNITY_METHODS

    private void Start()
    {
        InitializeManagers();
        LateInitializeManagers();

        Initialized = true;
        OnManagersInitialized?.Invoke();
    }

    private void OnDestroy()
    {
        CleanUpManagers();
    }

    #endregion

    #region METHODS

    public void InitializeManagers()
    {
        if (managers == null)
            managers = new();

        SetManagers();

        for (int i = 0; i < managers.Count; i++)
            managers[i].Initialzie();
    }

    public void LateInitializeManagers()
    {
        for (int i = 0; i < managers.Count; i++)
            managers[i].LateInitialzie();
    }

    public void CleanUpManagers()
    {
        for (int i = 0; i < managers.Count; i++)
            managers[i].CleanUp();
    }

    protected abstract void SetManagers();

    #endregion
}
