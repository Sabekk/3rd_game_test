using Gameplay.Cameras;
using Gameplay.Character;
using Gameplay.GameStates;
using Gameplay.Inputs;
using Gameplay.Items;
using Gameplay.Timing;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoSingleton<MainManager>
{
    #region ACTION

    public event Action OnGameplayManagersInitialized;

    #endregion

    #region VARIABLES

    [SerializeField] private List<IGameplayManager> managers = new();

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
        OnGameplayManagersInitialized?.Invoke();
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

        managers.Add(ItemsManager.Instance);
        managers.Add(InputManager.Instance);
        managers.Add(CharacterManager.Instance);
        managers.Add(CamerasManager.Instance);
        managers.Add(TimeManager.Instance);
        managers.Add(GameStateManager.Instance);


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

    #endregion
}
