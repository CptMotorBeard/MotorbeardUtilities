using System.Collections.Generic;
using BeardKit;
using UnityEditor;
using UnityEngine;

namespace BeardKitEditor
{
    [CustomEditor(typeof(MonoBehaviourEventComponent))]
    public class MonoBehaviourEventComponentEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Assign Fields"))
            {
                AssignFields(target as MonoBehaviourEventComponent);
            }
        }

        public static void AssignFields(MonoBehaviourEventComponent component)
        {
            var awakeListeners = new List<ScriptableObject>();
            var updateListeners = new List<ScriptableObject>();
            var destroyListeners = new List<ScriptableObject>();

            string[] guids = AssetDatabase.FindAssets("t: ScriptableObject");
            foreach (string guid in guids)
            {
                var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(guid));
                if (asset != null)
                {
                    if (asset is IBehaviourAwakeListener)
                    {
                        awakeListeners.Add(asset);
                    }

                    if (asset is IBehaviourUpdateListener)
                    {
                        updateListeners.Add(asset);
                    }

                    if (asset is IBehaviourDestroyListener)
                    {
                        destroyListeners.Add(asset);
                    }
                }
            }

            EditorTools.SetPrivateValue(component, "m_awakeListeners", awakeListeners.ToArray());
            EditorTools.SetPrivateValue(component, "m_updateListeners", updateListeners.ToArray());
            EditorTools.SetPrivateValue(component, "m_destroyListeners", destroyListeners.ToArray());
            EditorUtility.SetDirty(component);
        }
    }
}