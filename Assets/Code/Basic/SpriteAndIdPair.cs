using System;
using UnityEngine;

[Serializable]
public class SpriteAndIdPair : IIdEqualable
{
    #region VARIABLES

    [SerializeField] private int id;
    [SerializeField] private Sprite sprite;

    #endregion

    #region PROPERTIES

    public int Id => id;
    public Sprite Sprite => sprite;

    #endregion

    #region METHODS

    public bool IdEquals(int id)
    {
        return Id == id;
    }


    #endregion
}
