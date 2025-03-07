using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectionExtensions
{

    public static void SetActiveOptimizeLastElement<T>(this IList<T> list, bool state) where T : MonoBehaviour
    {
        T lastElement = GetLastElement(list);
        if (lastElement)
            lastElement.gameObject.SetActiveOptimize(state);
    }

    public static T GetLastElement<T>(this IList<T> list) where T : MonoBehaviour
    {
        if (list.Count <= 0)
            return null;

        T lastElement = list[list.Count - 1];
        return lastElement;
    }
    public static bool ContainsId<T>(this IList<T> list, int id) where T : IIdEqualable
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].IdEquals(id))
                return true;
        }
        return false;
    }

    public static T GetElementById<T>(this IList<T> list, int id) where T : IIdEqualable
    {
        if (list != null)
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].IdEquals(id))
                    return list[i];
            }
        return default(T);
    }
}
