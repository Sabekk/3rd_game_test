using Gameplay.GameStates;
using Gameplay.Inputs;
using Gameplay.Scenes;
using UI;

public class GlobalManagersParent : ManagersParent
{
    #region VARIABLES

    #endregion

    #region PROPERTIES

    #endregion

    #region UNITY_METHODS

    protected override void Awake()
    {
        CheckDontDestroy();
    }


    #endregion

    #region METHODS

    protected override void SetManagers()
    {
        managers.Add(UIManager.Instance);
        managers.Add(ScenesManager.Instance);
        managers.Add(InputManager.Instance);
        managers.Add(GameStateManager.Instance);
    }

    private bool CheckDontDestroy()
    {
        GlobalManagersParent[] objs = FindObjectsOfType<GlobalManagersParent>();
        if (objs.Length > 1)
        {
            gameObject.SetActive(false);
            DestroyImmediate(gameObject);
            return false;
        }

        DontDestroyOnLoad(gameObject);
        return true;
    }


    #endregion
}
