using System.Collections.Generic;
using UnityEngine;
using static BeardKit.DDOLScriptableObject;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace BeardKit
{
    public static class DDOLScriptableObject
    {
        // By default, unity unloads non-referenced scriptable objects on scene changes. With a static reference we can prevent this
        // Adding and removing (using the DDOLContext) is O(1)
        // If for some reason storing the DDOLContext is impossible, it is also allowed to remove using an SO reference at O(n)

        private static readonly LinkedList<ScriptableObject> s_dontDestroyOnLoad = new LinkedList<ScriptableObject>();

#if UNITY_EDITOR
        // Allows for disabling domain reload while still using this static class

        [InitializeOnEnterPlayMode]
        private static void ResetList(EnterPlayModeOptions options)
        {
            if (options.HasFlag(EnterPlayModeOptions.DisableDomainReload))
            {
                s_dontDestroyOnLoad.Clear();
            }
        }
#endif

        internal static DDOLContext DontDestroyOnLoad(ScriptableObject context)
        {
            return new DDOLContext(s_dontDestroyOnLoad.AddLast(context));
        }

        internal static void AllowDestroyOnLoad(DDOLContext context)
        {
            s_dontDestroyOnLoad.Remove(context.Node);
        }

        internal static void AllowDestroyOnLoad(ScriptableObject context)
        {
            s_dontDestroyOnLoad.Remove(context);
        }

        public readonly struct DDOLContext
        {
            internal readonly LinkedListNode<ScriptableObject> Node;

            internal DDOLContext(LinkedListNode<ScriptableObject> node)
            {
                Node = node;
            }

            public bool IsValid()
            {
                return Node != null;
            }

            public static DDOLContext Empty => new DDOLContext(null);
        }
    }

    public static class ScriptableObjectExtensions
    {
        public static DDOLContext DontDestroyOnLoad(this ScriptableObject scriptableObject)
        {
            return DDOLScriptableObject.DontDestroyOnLoad(scriptableObject);
        }

        public static void AllowDestroyOnLoad(this ScriptableObject scriptableObject, DDOLContext context)
        {
            DDOLScriptableObject.AllowDestroyOnLoad(context);
        }

        public static void AllowDestroyOnLoad(this DDOLContext scriptableObject)
        {
            DDOLScriptableObject.AllowDestroyOnLoad(scriptableObject);
        }

        /// <summary>
        /// Prefer <see cref="AllowDestroyOnLoad(DDOLContext)" /> or
        /// <see cref="AllowDestroyOnLoad(ScriptableObject, DDOLContext)" /> over this method as those are O(1). This function is
        /// O(n)
        /// </summary>
        public static void AllowDestroyOnLoad(this ScriptableObject scriptableObject)
        {
            DDOLScriptableObject.AllowDestroyOnLoad(scriptableObject);
        }
    }

    public abstract class NonPersistentScriptableObject : ScriptableObject
    {
        protected virtual void OnEnable()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
#endif
        }

        protected virtual void OnDisable()
        {
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
#endif
        }

#if UNITY_EDITOR
        // Save all the assets when we enter playmode (Unity does not do this by default). When we exit playmode, we can unload
        // the scriptable object, which will reset the values back to what is saved to disk.

        [InitializeOnEnterPlayMode]
        private static void SaveAssets()
        {
            AssetDatabase.SaveAssets();
        }

        private void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                Resources.UnloadAsset(this);
            }
        }
#endif
    }
}