using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
   public static void SetActiveOptimize(this GameObject gameObject, bool state)
    {
        if (gameObject.activeInHierarchy == state)
            return;

        gameObject.SetActive(state);
    }

    public static void DestroyChildren(this Transform transform)
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
        }
    }

    public static void DestroyChildrenImmediate(this Transform transform)
    {
        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            UnityEngine.Object.DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
