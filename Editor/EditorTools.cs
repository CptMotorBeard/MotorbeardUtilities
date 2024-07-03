using System;
using System.Reflection;
using BeardKit;

namespace BeardKitEditor
{
    public static class EditorTools
    {
        public static void SetPrivateValue(object obj, string key, object value)
        {
            Type type = obj.GetType();
            while (type != null)
            {
                FieldInfo field = type.GetField(key, BindingFlags.NonPublic | BindingFlags.Instance);
                if (field != null)
                {
                    field.SetValue(obj, value);
                    return;
                }

                type = type.BaseType;
            }

            Assert.Fail($"No field found with name {key}, in type {obj.GetType().Name}");
        }
    }
}