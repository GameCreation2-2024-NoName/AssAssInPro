using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HighlightPlus2D {
    [CustomEditor(typeof(HighlightManager2D))]
    public class HighlightManagerEditor : Editor {
        public override void OnInspectorGUI() {
            EditorGUILayout.Separator();
            EditorGUILayout.HelpBox("Only objects with a collider can be highlighted automatically.", MessageType.Info);
            DrawDefaultInspector();
        }
    }
}
