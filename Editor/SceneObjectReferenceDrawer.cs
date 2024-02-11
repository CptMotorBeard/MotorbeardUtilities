using UnityEditor;
using UnityEngine;

namespace BeardKitEditor
{
    [CustomPropertyDrawer(typeof(BeardKit.SceneObjectReference))]
    public class SceneObjectReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty serializedPath = property.FindPropertyRelative("ScenePath");
            string scenePath = serializedPath.stringValue;
            var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);

            EditorGUI.BeginChangeCheck();
            var newScene = EditorGUILayout.ObjectField(label, oldScene, typeof(SceneAsset), true);

            if (EditorGUI.EndChangeCheck())
            {
                var newPath = AssetDatabase.GetAssetPath(newScene);
                serializedPath.stringValue = newPath;
            }
        }
    }
}