using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BeardKitEditor
{
    public static class SerializedPropertyExtensions
    {
        public static T GetPropertyAttribute<T>(this SerializedProperty property, bool inherit) where T : PropertyAttribute
        {
            if (property == null)
            {
                return null;
            }

            Type type = property.serializedObject.targetObject.GetType();
            MemberInfo memberInfo = null;

            foreach (string name in property.propertyPath.Split('.'))
            {
                memberInfo = type.GetField(name, (BindingFlags)(-1));

                if (memberInfo == null)
                {
                    memberInfo = type.GetProperty(name, (BindingFlags)(-1));
                    if (memberInfo == null)
                    {
                        return null;
                    }

                    type = memberInfo.DeclaringType;
                }
            }

            T[] attributes;
            if (memberInfo != null)
            {
                attributes = memberInfo.GetCustomAttribute<T>(inherit) as T[];
            }
            else
            {
                return null;
            }

            return attributes.Length > 0 ? attributes[0] : null;
        }
    }
}