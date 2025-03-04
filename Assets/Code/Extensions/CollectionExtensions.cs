using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectionExtensions
{
    public static bool ContainsId<T>(this IList<T> list, int id) where T : IIdEqualable
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].IdEquals(id))
                return true;
        }
        return false;
    }
}
