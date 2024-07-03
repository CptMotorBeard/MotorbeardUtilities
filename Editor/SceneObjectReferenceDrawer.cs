using BeardKit;
using UnityEditor;
using UnityEngine;

namespace BeardKitEditor
{
    [CustomPropertyDrawer(typeof(SceneObjectReference))]
    public class SceneObjectReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty serializedPath = property.FindPropertyRelative("ScenePath");
            string scenePath = serializedPath.stringValue;
            var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);

            EditorGUI.BeginChangeCheck();
            Object newScene = EditorGUILayout.ObjectField(label, oldScene, typeof(SceneAsset), true);

            if (EditorGUI.EndChangeCheck())
            {
                string newPath = AssetDatabase.GetAssetPath(newScene);
                serializedPath.stringValue = newPath;
            }
        }
    }
}