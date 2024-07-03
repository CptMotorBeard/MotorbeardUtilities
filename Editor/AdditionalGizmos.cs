using UnityEditor;
using UnityEngine;

namespace BeardKitEditor
{
    /// <summary>A collection of Editor only Gizmos</summary>
    public static class AdditionalGizmos
    {
        /// <summary>Editor Only</summary>
        public static void DrawArrow(Vector3 start, Vector3 end, float lineWidth = 6, float arrowHeadLength = 5f,
            float arrowHeadAngle = 30.0f)
        {
            DrawThickLine(start, end, lineWidth);

            Vector3 right = Quaternion.LookRotation(end - start) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * new Vector3(0, 0, 1);
            Vector3 left = Quaternion.LookRotation(end - start) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * new Vector3(0, 0, 1);

            DrawThickLine(end, end + right * arrowHeadLength, lineWidth);
            DrawThickLine(end, end + left * arrowHeadLength, lineWidth);
        }

        /// <summary>Editor Only</summary>
        public static void DrawThickLine(Vector3 start, Vector3 end, float width)
        {
            Handles.DrawBezier(start, end, start, end, Gizmos.color, null, width);
        }
    }
}