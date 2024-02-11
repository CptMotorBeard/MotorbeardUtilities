using BeardKit;
using System.Collections.Generic;
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
                List<IBehaviourAwakeListener> awakeListeners = new List<IBehaviourAwakeListener>();
                List<IBehaviourUpdateListener> updateListeners = new List<IBehaviourUpdateListener>();
                List<IBehaviourDestroyListener> destroyListeners = new List<IBehaviourDestroyListener>();

                string[] guids = AssetDatabase.FindAssets("t: ScriptableObject");
                foreach (string guid in guids)
                {
                    var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(guid));
                    if (asset != null)
                    {
                        if (asset is IBehaviourAwakeListener awakeListener)
                        {
                            awakeListeners.Add(awakeListener);
                        }

                        if (asset is IBehaviourUpdateListener updateListener)
                        {
                            updateListeners.Add(updateListener);
                        }

                        if (asset is IBehaviourDestroyListener destroyListener)
                        {
                            destroyListeners.Add(destroyListener);
                        }
                    }
                }

                var component = target as MonoBehaviourEventComponent;
                EditorTools.SetPrivateValue(component, "m_awakeListeners", awakeListeners.ToArray());
                EditorTools.SetPrivateValue(component, "m_updateListeners", updateListeners.ToArray());
                EditorTools.SetPrivateValue(component, "m_destroyListeners", destroyListeners.ToArray());
                EditorUtility.SetDirty(component);
            }
        }
    }
}