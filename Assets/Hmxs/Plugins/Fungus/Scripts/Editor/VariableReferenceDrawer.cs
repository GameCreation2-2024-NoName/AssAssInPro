// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEditor;
using UnityEngine;

namespace Fungus.EditorUtils
{
    /// <summary>
    /// Custom drawer for the VariableReference, allows for more easily selecting a target variable in external c#
    /// scripts.
    /// </summary>
    [CustomPropertyDrawer(typeof(Fungus.VariableReference))]
    public class VariableReferenceDrawer : PropertyDrawer
    {
        //public Fungus.Flowchart lastFlowchart;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var l = EditorGUI.BeginProperty(position, label, property);
            var startPos = position;
            position = EditorGUI.PrefixLabel(position, l);
            position.height = EditorGUIUtility.singleLineHeight;
            var variable = property.FindPropertyRelative("variable");
            var flowchartProp = property.FindPropertyRelative("flowchart");

            Fungus.Variable v = variable.objectReferenceValue as Fungus.Variable;
            Fungus.Flowchart lastFlowchart = flowchartProp.objectReferenceValue as Fungus.Flowchart;



            EditorGUI.PropertyField(position, flowchartProp, GUIContent.none);
            position.y += EditorGUIUtility.singleLineHeight;
            if (lastFlowchart != null)
            {
                if (!property.serializedObject.isEditingMultipleObjects)
                {
                    var ourPos = startPos;
                    ourPos.y = position.y;
                    ourPos.yMax -= EditorGUIUtility.singleLineHeight;//有待.
                    var prefixLabel = new GUIContent(v != null ? v.GetType().Name : "No Var Selected");
                    EditorGUI.indentLevel++;
                    VariableEditor.VariableField(variable,
                                                 prefixLabel,
                                                 lastFlowchart,
                                                 "<None>",
                                                 null,
                                                 //lable, index, elements
                                                 (s, t, u) => (EditorGUI.Popup(ourPos, s, t, u)));


                    EditorGUI.indentLevel--;
                }
            }
            else
            {
                EditorGUI.PrefixLabel(position, new GUIContent("Flowchart Required"));
            }

            variable.serializedObject.ApplyModifiedProperties();
            property.serializedObject.ApplyModifiedProperties();
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2;
        }
    }
}