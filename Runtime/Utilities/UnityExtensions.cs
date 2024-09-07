using System.Collections;
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

    public static class RectTransformExtensions
    {
        #region Left, Right, Top, Bottom

        public static void SetLeft(this RectTransform rectTransform, float left)
        {
            rectTransform.offsetMin = rectTransform.offsetMin.With(x: left);
        }

        public static void SetRight(this RectTransform rectTransform, float right)
        {
            rectTransform.offsetMax = rectTransform.offsetMin.With(x: -right);
        }

        public static void SetTop(this RectTransform rectTransform, float top)
        {
            rectTransform.offsetMax = rectTransform.offsetMax.With(y: -top);
        }

        public static void SetBottom(this RectTransform rectTransform, float bottom)
        {
            rectTransform.offsetMin = rectTransform.offsetMin.With(y: bottom);
        }

        public static float GetLeft(this RectTransform rectTransform)
        {
            return rectTransform.offsetMin.x;
        }

        public static float GetRight(this RectTransform rectTransform)
        {
            return -rectTransform.offsetMax.x;
        }

        public static float GetTop(this RectTransform rectTransform)
        {
            return -rectTransform.offsetMax.y;
        }

        public static float GetBottom(this RectTransform rectTransform)
        {
            return rectTransform.offsetMin.y;
        }

        #endregion

        #region Anchor Offsets

        public static void SetLeftAnchorOffset(this RectTransform rectTransform, float leftPercent)
        {
            rectTransform.anchorMin = rectTransform.anchorMin.With(x: leftPercent);
        }

        public static void SetRightAnchorOffset(this RectTransform rectTransform, float rightPercent)
        {
            rectTransform.anchorMax = rectTransform.anchorMax.With(x: 1f - rightPercent);
        }

        public static void SetTopAnchorOffset(this RectTransform rectTransform, float topPercent)
        {
            rectTransform.anchorMax = rectTransform.anchorMax.With(y: 1f - topPercent);
        }

        public static void SetBottomAnchorOffset(this RectTransform rectTransform, float bottomPercent)
        {
            rectTransform.anchorMin = rectTransform.anchorMin.With(y: bottomPercent);
        }

        public static void SetAnchorOffset(this RectTransform rectTransform, float left, float top, float right, float bottom)
        {
            rectTransform.anchorMin = Vector2.zero.With(x: left, y: bottom);
            rectTransform.anchorMax = Vector2.zero.With(x: 1 - right, y: 1 - top);
        }

        #endregion

        #region World Position

        // Clockwise, starting from bottom left
        private static readonly Vector3[] s_fourCorners = new Vector3[4];

        public static Vector2 GetWorldCenter(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(s_fourCorners);
            float x = (s_fourCorners[0].x + s_fourCorners[3].x) / 2f;
            float y = (s_fourCorners[0].y + s_fourCorners[1].y) / 2f;
            return Vector2.zero.With(x, y);
        }

        public static float GetWorldLeft(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(s_fourCorners);
            return s_fourCorners[0].x;
        }

        public static float GetWorldRight(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(s_fourCorners);
            return s_fourCorners[2].x;
        }

        public static float GetWorldTop(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(s_fourCorners);
            return s_fourCorners[1].y;
        }

        public static float GetWorldBottom(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(s_fourCorners);
            return s_fourCorners[0].y;
        }

        public static Vector2 GetWorldTopLeft(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(s_fourCorners);
            float x = s_fourCorners[0].x;
            float y = s_fourCorners[1].y;
            return Vector2.zero.With(x, y);
        }

        public static Vector2 GetWorldTopRight(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(s_fourCorners);
            float x = s_fourCorners[2].x;
            float y = s_fourCorners[1].y;
            return Vector2.zero.With(x, y);
        }

        public static Vector2 GetWorldBottomLeft(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(s_fourCorners);
            float x = s_fourCorners[0].x;
            float y = s_fourCorners[0].y;
            return Vector2.zero.With(x, y);
        }

        public static Vector2 GetWorldBottomRight(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(s_fourCorners);
            float x = s_fourCorners[2].x;
            float y = s_fourCorners[0].y;
            return Vector2.zero.With(x, y);
        }

        public static Rect GetWorldRect(this RectTransform rectTransform)
        {
            rectTransform.GetWorldCorners(s_fourCorners);

            float x = s_fourCorners[0].x;
            float y = s_fourCorners[0].y;

            float width = Mathf.Abs(s_fourCorners[3].x - s_fourCorners[0].x);
            float height = Mathf.Abs(s_fourCorners[1].y - s_fourCorners[0].y);

            return new Rect(x, y, width, height);
        }

        #endregion
    }

    public static class CollectionExtensions
    {
        public static void DestroyAllGameObjectsAndClear<T>(this List<T> list) where T : Behaviour
        {
            foreach (T item in list)
            {
                UnityObject.Destroy(item.gameObject);
            }

            list.Clear();
        }

        public static void DestroyAllGameObjectsAndClear<T>(this T[] array) where T : Behaviour
        {
            for (var i = 0; i < array.Length; ++i)
            {
                UnityObject.Destroy(array[i].gameObject);
                array[i] = null;
            }
        }

        public static void DestroyAllAndClear<T>(this List<T> list) where T : UnityObject
        {
            foreach (T item in list)
            {
                UnityObject.Destroy(item);
            }

            list.Clear();
        }

        public static void DestroyAllAndClear<T>(this T[] array) where T : UnityObject
        {
            for (var i = 0; i < array.Length; ++i)
            {
                UnityObject.Destroy(array[i]);
                array[i] = null;
            }
        }

        /// <summary>Casts a list to the type TResult.</summary>
        /// <exception cref="System.InvalidCastException">Throws an exception if the cast is invalid</exception>
        public static List<TResult> Cast<TResult>(this IList sourceList)
        {
            var resultList = new List<TResult>(sourceList.Count);
            foreach (TResult item in sourceList)
            {
                resultList.Add(item);
            }

            return resultList;
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

        public static Vector3 Add(this Vector3 v, Vector3 w)
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

        public static Vector3 Subtract(this Vector3 v, Vector3 w)
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