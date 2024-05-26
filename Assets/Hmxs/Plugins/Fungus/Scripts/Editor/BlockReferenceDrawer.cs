// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Fungus.EditorUtils
{
    /// <summary>
    /// Custom drawer for the BlockReference, allows for more easily selecting a target block in external c#
    /// scripts.
    /// </summary>
    [CustomPropertyDrawer(typeof(Fungus.BlockReference))]
    public class BlockReferenceDrawer : PropertyDrawer
    {
        //public Fungus.Flowchart lastFlowchart;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var l = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, l);
            position.height = EditorGUIUtility.singleLineHeight;
            var block = property.FindPropertyRelative("block");
            var flowchartProp = property.FindPropertyRelative("flowchart");

            Fungus.Block b = block.objectReferenceValue as Fungus.Block;
            Fungus.Flowchart lastFlowchart = flowchartProp.objectReferenceValue as Fungus.Flowchart;


            EditorGUI.PropertyField(position, flowchartProp, GUIContent.none);
            position.y += EditorGUIUtility.singleLineHeight;
            if (flowchartProp.objectReferenceValue != null && !property.serializedObject.isEditingMultipleObjects)
                b = Fungus.EditorUtils.BlockEditor.BlockField(position, new GUIContent("None"), lastFlowchart, b);
            else
                EditorGUI.PrefixLabel(position, new GUIContent("Flowchart Required"));

            if (!property.serializedObject.isEditingMultipleObjects)
            block.objectReferenceValue = b;

            block.serializedObject.ApplyModifiedProperties();
            property.serializedObject.ApplyModifiedProperties();
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 2;
        }
    }
}