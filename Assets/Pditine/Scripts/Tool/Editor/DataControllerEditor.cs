// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_10_14
// -------------------------------------------------
#if UNITY_EDITOR
using PurpleFlowerCore;
using UnityEditor;
using UnityEngine;

namespace Pditine.Tool
{

    [CustomEditor(typeof(DataController))]
    public class DataControllerEditor : Editor
    {
        public DataController DataController => target as DataController;
        public ScriptableObject ScriptableObject => target as ScriptableObject;
        public override void OnInspectorGUI()
        {
            Head();
            base.OnInspectorGUI();
            //PFCLog.Info();
        }

        private void Head()
        {
            var fontStyle = new GUIStyle();
            fontStyle.fontSize = 20;
            fontStyle.normal.textColor = Color.green;
            GUILayout.Label("综合数值控制器", fontStyle);
        }

        private void ShowData()
        {
            
        }
        
        private void Refresh()
        {

        }
    }

}
#endif