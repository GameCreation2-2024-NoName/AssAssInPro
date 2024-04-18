using UnityEditor;
using UnityEngine;
namespace PurpleFlowerCore.Editor
{
    public static class PFCMenu
    {
        [MenuItem("PFC/打开存档路径")]
        public static void OpenArchivedDirPath()
        {
            string path = Application.persistentDataPath.Replace("/","\\");
            System.Diagnostics.Process.Start("explorer.exe", path);
        }
        
        [MenuItem("PFC/联系作者")]
        public static void OpenDoc()
        {
            Application.OpenURL("https://purpleditine.top/");
        }
        
    }
}