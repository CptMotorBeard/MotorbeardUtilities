using System.Collections.Generic;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace BeardKit
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts a Unity pseudo-null to a real null, allowing for coalesce operators
        /// </summary>
        /// <returns><paramref name="obj" /> if it is not null, and an <see cref="object" /> real null if it is.</returns>
        public static T AsRealNull<T>(this T obj) where T : UnityObject
        {
            if (obj == null)
            {
                return null;
            }

            return obj;
        }
    }

    public static class GameObjectExtensions
    {
        /// <summary>
        /// As <see cref="UnityEngine.Transform.Find(string)" /> but returns a <see cref="UnityEngine.GameObject" />
        /// instead
        /// </summary>
        public static GameObject Find(this GameObject parent, string name)
        {
            Transform foundTransform = parent.transform.Find(name);
            if (foundTransform)
            {
                return foundTransform.gameObject;
            }

            return null;
        }

        /// <summary>
        /// As <see cref="TransformExtensions.TryFind(Transform, string, out Transform)" /> but returns a
        /// <see cref="UnityEngine.GameObject" /> instead
        /// </summary>
        public static bool TryFind(this GameObject parent, string name, out GameObject foundGameObject)
        {
            if (parent.transform.TryFind(name, out Transform foundTransform))
            {
                foundGameObject = foundTransform.gameObject;
                return true;
            }

            foundGameObject = null;
            return false;
        }

        /// <summary>Tries to get a component and adds it if it is not found</summary>
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (!gameObject.TryGetComponent(out T component))
            {
                component = gameObject.AddComponent<T>();
            }

            return component;
        }
    }

    public static class TransformExtensions
    {
        /// <summary>Finds a child by name n and if it exists return true;</summary>
        /// <returns>True if the child transform was found and false otherwise</returns>
        public static bool TryFind(this Transform transform, string n, out Transform foundTransform)
        {
            foundTransform = transform.Find(n);
            return foundTransform != null;
        }

        /// <summary>Resets the position, scale and rotation</summary>
        public static void Reset(this Transform transform)
        {
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }

    public static class CollectionExtensions
    {
        public static void DestroyAllGameObjectsAndClear<T>(this List<T> list) where T : Behaviour
        {
            foreach (T item in list)
            {
                Object.Destroy(item.gameObject);
            }

            list.Clear();
        }

        public static void DestroyAllGameObjectsAndClear<T>(this T[] array) where T : Behaviour
        {
            for (var i = 0; i < array.Length; ++i)
            {
                Object.Destroy(array[i].gameObject);
                array[i] = null;
            }
        }

        public static void DestroyAllAndClear<T>(this List<T> list) where T : Object
        {
            foreach (T item in list)
            {
                Object.Destroy(item);
            }

            list.Clear();
        }

        public static void DestroyAllAndClear<T>(this T[] array) where T : Object
        {
            for (var i = 0; i < array.Length; ++i)
            {
                Object.Destroy(array[i]);
                array[i] = null;
            }
        }
    }

    public static class VectorExtensions
    {
        public static Vector2 With(this Vector2 v, float? x = null, float? y = null)
        {
            Vector2 newVector;
            newVector.x = x ?? v.x;
            newVector.y = y ?? v.y;

            return newVector;
        }

        public static Vector3 With(this Vector3 v, float? x = null, float? y = null, float? z = null)
        {
            Vector3 newVector;
            newVector.x = x ?? v.x;
            newVector.y = y ?? v.y;
            newVector.z = z ?? v.z;

            return newVector;
        }

        public static Vector2 Add(this Vector2 v, float? x = null, float? y = null)
        {
            Vector2 newVector;
            newVector.x = v.x + (x ?? 0);
            newVector.y = v.y + (y ?? 0);

            return newVector;
        }

        public static Vector2 Add(this Vector2 v, Vector2 w)
        {
            Vector2 newVector;
            newVector.x = v.x + w.x;
            newVector.y = v.y + w.y;

            return newVector;
        }

        public static Vector3 Add(this Vector3 v, float? x = null, float? y = null, float? z = null)
        {
            Vector3 newVector;
            newVector.x = v.x + (x ?? 0);
            newVector.y = v.y + (y ?? 0);
            newVector.z = v.z + (z ?? 0);

            return newVector;
        }

        public static Vector2 Add(this Vector3 v, Vector3 w)
        {
            Vector3 newVector;
            newVector.x = v.x + w.x;
            newVector.y = v.y + w.y;
            newVector.z = v.z + w.z;

            return newVector;
        }

        public static Vector2 Subtract(this Vector2 v, float? x = null, float? y = null)
        {
            Vector2 newVector;
            newVector.x = v.x - (x ?? 0);
            newVector.y = v.y - (y ?? 0);

            return newVector;
        }

        public static Vector2 Subtract(this Vector2 v, Vector2 w)
        {
            Vector2 newVector;
            newVector.x = v.x - w.x;
            newVector.y = v.y - w.y;

            return newVector;
        }

        public static Vector3 Subtract(this Vector3 v, float? x = null, float? y = null, float? z = null)
        {
            Vector3 newVector;
            newVector.x = v.x - (x ?? 0);
            newVector.y = v.y - (y ?? 0);
            newVector.z = v.z - (z ?? 0);

            return newVector;
        }

        public static Vector2 Subtract(this Vector3 v, Vector3 w)
        {
            Vector3 newVector;
            newVector.x = v.x - w.x;
            newVector.y = v.y - w.y;
            newVector.z = v.z - w.z;

            return newVector;
        }
    }

    public static class NumberExtensions
    {
        public static void ThisAtLeast(this ref int value, int min) => value = Mathf.Max(value, min);
        public static void ThisAtLeast(this ref float value, float min) => value = Mathf.Max(value, min);
        public static void ThisAtLeast(this ref double value, double min) => value = MathfExtension.Max(value, min);

        public static int AtLeast(this int value, int min) => Mathf.Max(value, min);
        public static float AtLeast(this float value, float min) => Mathf.Max(value, min);
        public static double AtLeast(this double value, double min) => MathfExtension.Max(value, min);

        public static void ThisAtMost(this ref int value, int max) => value = Mathf.Min(value, max);
        public static void ThisAtMost(this ref float value, float max) => value = Mathf.Min(value, max);
        public static void ThisAtMost(this ref double value, double max) => value = MathfExtension.Min(value, max);

        public static int AtMost(this int value, int max) => Mathf.Min(value, max);
        public static float AtMost(this float value, float max) => Mathf.Min(value, max);
        public static double AtMost(this double value, double max) => MathfExtension.Min(value, max);
    }

    public struct MathfExtension
    {
        public static double Min(double a, double b)
        {
            return a < b ? a : b;
        }

        public static double Max(double a, double b)
        {
            return a > b ? a : b;
        }
    }
}