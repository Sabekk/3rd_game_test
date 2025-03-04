public abstract class Singleton<T> where T : Singleton<T>, new()
{
    #region VARIABLES

    protected static T _instance;

    #endregion

    #region PROPERTIES

    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = new T();
            return _instance;
        }
    }

    #endregion
}
