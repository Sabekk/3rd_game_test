using UnityEngine;

public class GlobalManagerParent : MonoBehaviour
{
    #region UNITY_METHODS

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    #endregion
}
