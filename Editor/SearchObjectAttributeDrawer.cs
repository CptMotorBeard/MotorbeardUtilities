using System;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SearchObjectAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        position.width -= 60;
        EditorGUI.ObjectField(position, property, label);

        position.x += position.width;
        position.width = 60;

        if (GUI.Button(position, new GUIContent("Find")))
        {
            Type type = property.GetPropertyAttribute<SearchObjectAttribute>(true).SearchObjectType;
            SearchWindow.Open(new SearchWindowContext(GUIUtility.GUIToScreenPoint(Event.current.mousePosition)), new ObjectSearchProvider(type, property));
        }
    }
}
