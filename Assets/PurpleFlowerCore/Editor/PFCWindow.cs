using UnityEditor;
using UnityEngine;

namespace PurpleFlowerCore.Editor
{
    
    public class PFCWindow : EditorWindow
    {
        private SaveMode _saveMode;
        private bool _logInfo;
        private bool _logWarning;
        private bool _logError;
        
        [MenuItem("PFC/打开设置菜单")]
        public static void OpenWindow()
        {
            var win = GetWindow<PFCWindow>("Purple Flower Core");
            win.Show();
            win.maxSize = new Vector2(800, 600);
            win.minSize = win.maxSize;
        }
        
        private void OnEnable()
        {
            Init();
        }

        private void Init()
        {
            _saveMode = PFCSetting.Instance.SaveMode;
            _logInfo = PFCSetting.Instance.LogInfo;
            _logWarning = PFCSetting.Instance.LogWarning;
            _logError = PFCSetting.Instance.LogError;
        }

        private void OnGUI()
        {
            GUI.DrawTexture(new Rect(600,400,200,200),Resources.Load<Texture>("PFCRes/logo-a0"));
            EditorGUILayout.LabelField("欢迎使用PurpleFlowerCore，这是我为了更好的个性化而制作的程序框架和工具集合\n当前处于早期开发版", GUILayout.Height(50));
            _saveMode = (SaveMode)EditorGUILayout.EnumPopup("数据持久化方式", _saveMode);
            _logInfo = EditorGUILayout.Toggle("使用LogInfo", _logInfo);
            _logWarning = EditorGUILayout.Toggle("使用LogWarning", _logWarning);
            _logError = EditorGUILayout.Toggle("使用LogError", _logError);
            
            //EditorGUILayout.BeginVertical(EditorGUILayout.TextField(,));
            
            if (GUILayout.Button("应用"))
                Apply();
        }

        public void Apply()
        {
            ChangeSaveMode();
            ChangeLogMode();
            PFCSetting.ReSet();
        }
        
        private void ChangeSaveMode()
        {
            RemoveScriptCompilationSymbol("PFC_SAVE_" + PFCSetting.Instance.SaveMode.ToString().ToUpper());
            PFCSetting.Instance.SaveMode = _saveMode;
            AddScriptCompilationSymbol("PFC_SAVE_" + _saveMode.ToString().ToUpper());
        }

        private void ChangeLogMode()
        {
            if(_logInfo)AddScriptCompilationSymbol("PFC_LOG_INFO");
            else RemoveScriptCompilationSymbol("PFC_LOG_INFO");
            if(_logWarning)AddScriptCompilationSymbol("PFC_LOG_WARNING");
            else RemoveScriptCompilationSymbol("PFC_LOG_WARNING");
            if(_logError)AddScriptCompilationSymbol("PFC_LOG_ERROR");
            else RemoveScriptCompilationSymbol("PFC_LOG_ERROR");
            PFCSetting.Instance.LogInfo = _logInfo;
            PFCSetting.Instance.LogWarning = _logWarning;
            PFCSetting.Instance.LogError = _logError;
        }
        
        /// <summary>
        /// 增加预处理指令
        /// </summary>
        private static void AddScriptCompilationSymbol(string symbolName)
        {
            BuildTargetGroup buildTargetGroup = UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup;
            string group = UnityEditor.PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            if (!group.Contains(symbolName))
            {
                UnityEditor.PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, group + ";" + symbolName);
            }
        }

        /// <summary>
        /// 移除预处理指令
        /// </summary>
        private static void RemoveScriptCompilationSymbol(string symbolName)
        {
            BuildTargetGroup buildTargetGroup = UnityEditor.EditorUserBuildSettings.selectedBuildTargetGroup;
            string group = UnityEditor.PlayerSettings.GetScriptingDefineSymbolsForGroup(buildTargetGroup);
            if (group.Contains(symbolName))
            {
                UnityEditor.PlayerSettings.SetScriptingDefineSymbolsForGroup(buildTargetGroup, group.Replace(";" + symbolName, string.Empty));
            }
        }

        [InitializeOnLoadMethod]
        private static void AutoOpen()
        {
            if (PFCSetting.Instance.HasStarted)
                return;
            PFCSetting.Instance.HasStarted = true;
            PFCSetting.ReSet();
            EditorApplication.delayCall += OpenWindow;
        }
    }
    
    // public class AutoOpenWindow : AssetPostprocessor
    // {
    //
    //     private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
    //         string[] movedAssets, string[] movedFromAssetPaths)
    //     {
    //         foreach (var asset in importedAssets)
    //         {
    //             if(asset is "PFCChecker.cs")
    //                 Debug.Log(true);
    //         }
    //         OpenWindow();
    //     }
    //     // static AutoOpenWindow()
    //     // {
    //     //     // EditorApplication.delayCall += DoOpenWindow;
    //     //     // EditorApplication.delayCall += () =>
    //     //     // {
    //     //     //     Debug.Log(111);
    //     //     // };
    //     //     Debug.Log(assetPath);
    //     // }
    //
    //     private static void OpenWindow()
    //     {
    //         PFCWindow.OpenWindow();
    //     }
    // }g

}