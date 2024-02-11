using BeardKit;
using UnityEditor;
using UnityEngine;

namespace BeardKitEditor
{
    [CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GameEvent e = target as GameEvent;
            if (GUILayout.Button("Emit Event"))
                e.Dispatch();
        }
    }
}
