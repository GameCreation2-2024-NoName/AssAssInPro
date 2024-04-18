using UnityEditor;
using UnityEngine;

namespace HighlightPlus2D {
	[CustomEditor (typeof(HighlightTrigger2D))]
	public class HighlightTriggerEditor : Editor {

		SerializedProperty triggerMode, highlightEvent, highlightDuration, raycastCamera, raycastSource;
		HighlightTrigger2D trigger;

		void OnEnable () {
			highlightEvent = serializedObject.FindProperty ("highlightEvent");
			highlightDuration = serializedObject.FindProperty ("highlightDuration");
			triggerMode = serializedObject.FindProperty ("triggerMode");
			raycastCamera = serializedObject.FindProperty ("raycastCamera");
			raycastSource = serializedObject.FindProperty ("raycastSource");
			trigger = (HighlightTrigger2D)target;
			trigger.Init ();
		}

		public override void OnInspectorGUI () {
			EditorGUILayout.Separator ();
			EditorGUILayout.HelpBox ("Only objects with a collider can be highlighted automatically.", MessageType.Info);
            
			serializedObject.Update ();

			EditorGUILayout.PropertyField (highlightEvent);
			EditorGUILayout.PropertyField (highlightDuration, new GUIContent("Max Duration (0=no limit)"));

			EditorGUILayout.PropertyField (triggerMode);
			if (triggerMode.boolValue) {
				EditorGUILayout.PropertyField (raycastCamera);
				EditorGUILayout.PropertyField (raycastSource);
			}

			if (serializedObject.ApplyModifiedProperties ()) {
				trigger.Init ();
			}
		}



	}
}
