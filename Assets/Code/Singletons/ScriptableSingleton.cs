using UnityEngine;

public class ScriptableSingleton<T> : ScriptableObject where T : Object
{
    #region VARIABLES

    protected static T _instance;

    #endregion

    #region PROPERTIES

    public static T Instance
    {
        get
        {
            GetInstance("");
            return _instance;
        }
    }

    #endregion

    #region METHODS

    protected static T GetInstance(string path)
    {
        if (_instance == null)
        {
            _instance = Resources.Load<T>(path);
            if (_instance == null)
                Debug.LogError("Called singleton doesn't exist!: " + typeof(T).ToString());
        }

        return _instance;
    }

    #endregion
}