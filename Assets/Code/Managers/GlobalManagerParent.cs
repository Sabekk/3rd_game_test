using UnityEngine;

public class GlobalManagerParent : MonoBehaviour
{
    #region UNITY_METHODS

    private void Awake()
    {
        CheckDontDestroy();
    }

    #region METHODS

    private bool CheckDontDestroy()
    {
        GlobalManagerParent[] objs = FindObjectsOfType<GlobalManagerParent>();
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

    #endregion
}
