using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace BeardKitEditor
{
    public class ObjectSearchProvider : ScriptableObject, ISearchWindowProvider
    {
        Type m_assetType;
        SerializedProperty m_serializedProperty;

        public ObjectSearchProvider(Type assetType, SerializedProperty property)
        {
            m_assetType = assetType;
            m_serializedProperty = property;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> list = new List<SearchTreeEntry>();

            string[] assetGUIDs = AssetDatabase.FindAssets($"t:{m_assetType.Name}");
            List<string> paths = new List<string>();

            foreach (string guid in assetGUIDs)
            {
                paths.Add(AssetDatabase.GUIDToAssetPath(guid));
            }

            paths.Sort((a, b) =>
            {
                string[] splits1 = a.Split('/');
                string[] splits2 = b.Split('/');

                for (int i = 0; i < splits1.Length; ++i)
                {
                    if (i >= splits2.Length)
                    {
                        return 1;
                    }

                    int value = splits1[i].CompareTo(splits2[i]);
                    if (value != 0)
                    {
                        if (splits1.Length != splits2.Length && (i == splits1.Length - 1 || i == splits2.Length - 1))
                        {
                            return splits1.Length < splits2.Length ? 1 : -1;
                        }

                        return value;
                    }
                }

                return 0;
            });

            HashSet<string> groups = new HashSet<string>();
            foreach (string path in paths)
            {
                string[] entryTitle = path.Split('/');
                string groupName = string.Empty;

                for (int i = 0; i < entryTitle.Length; ++i)
                {
                    groupName += entryTitle[i];
                    if (groups.Add(groupName))
                    {
                        list.Add(new SearchTreeGroupEntry(new GUIContent(entryTitle[i]), i + 1));
                    }

                    groupName += '/';
                }

                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
                SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(entryTitle.Last(), EditorGUIUtility.ObjectContent(obj, obj.GetType()).image));
                entry.level = entryTitle.Length;
                entry.userData = obj;
                list.Add(entry);
            }

            return list;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            m_serializedProperty.objectReferenceValue = (UnityEngine.Object)SearchTreeEntry.userData;
            m_serializedProperty.serializedObject.ApplyModifiedProperties();
            return true;
        }
    }
}