using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    /// <summary>Finds a child by name n and if it exists return true;</summary>
    /// <returns>True if the child transform was found and false otherwise</returns>
    public static bool TryFind(this Transform transform, string n, out Transform foundTransform)
    {
        foundTransform = transform.Find(n);
        return foundTransform != null;
    }
}

public static class CollectionExtensions
{
    public static void DestroyAllGameObjectsAndClear<T>(this List<T> list) where T : Behaviour
    {
        foreach (var item in list)
        {
            Object.Destroy(item.gameObject);
        }

        list.Clear();
    }

    public static void DestroyAllGameObjectsAndClear<T>(this T[] array) where T : Behaviour
    {
        for (int i = 0; i < array.Length; ++i)
        {
            Object.Destroy(array[i].gameObject);
            array[i] = null;
        }
    }

    public static void DestroyAllAndClear<T>(this List<T> list) where T : Object
    {
        foreach (var item in list)
        {
            Object.Destroy(item);
        }

        list.Clear();
    }

    public static void DestroyAllAndClear<T>(this T[] array) where T : Object
    {
        for (int i = 0; i < array.Length; ++i)
        {
            Object.Destroy(array[i]);
            array[i] = null;
        }
    }
}