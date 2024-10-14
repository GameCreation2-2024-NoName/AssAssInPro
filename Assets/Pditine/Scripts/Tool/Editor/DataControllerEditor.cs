// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_10_14
// -------------------------------------------------
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Pditine.Tool
{

    [CustomEditor(typeof(DataController))]
    public class DataControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Head();
            base.OnInspectorGUI();
            Function();
        }

        private void Head()
        {
            var fontStyle = new GUIStyle();
            fontStyle.fontSize = 20;
            fontStyle.normal.textColor = Color.green;
            GUILayout.Label("综合数值控制器", fontStyle);
        }
        
        private void Function()
        {
            // GUILayout.Space(20);
            // GUILayout.Label("功能区", EditorStyles.boldLabel);
            // GUILayout.Space(10);
            // GUILayout.BeginHorizontal();
            // if (GUILayout.Button("保存数据"))
            // {
            //     ((DataController) target).SaveData();
            // }
            //
            // if (GUILayout.Button("加载数据"))
            // {
            //     ((DataController) target).LoadData();
            // }
            //
            // GUILayout.EndHorizontal();
        }
    }

}
#endif