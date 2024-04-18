using UnityEngine;

namespace PurpleFlowerCore
{
    /// <summary>
    /// 请在Scripting Define Symbols中添加PFC_LOG_INFO,PFC_LOG_WARNING,PFC_LOG_ERROR以使用功能
    /// </summary>
    public class PFCLog : MonoBehaviour
    {
        public static void Info(object content)
        {
#if PFC_LOG_INFO
           Debug.Log($"[PFC_Info]:<color=#ffffff>{content}</color>"); 
#endif
        }
        public static void Info(object content,Color color)
        {
#if PFC_LOG_INFO
            Debug.Log($"[PFC_Info]:<color=#{ColorUtility.ToHtmlStringRGB(color)}>{content}</color>"); 
#endif
        }
        
        public static void Warning(object content)
        {
#if PFC_LOG_WARNING
            Debug.LogWarning($"[PFC_Warning]:<color=#ffffff>{content}</color>"); 
#endif
        }
        
        public static void Warning(object content,Color color)
        {
#if PFC_LOG_WARNING
            Debug.LogWarning($"[PFC_Warning]:<color=#{ColorUtility.ToHtmlStringRGB(color)}>{content}</color>"); 
#endif
        }
        
        public static void Error(object content)
        {
#if PFC_LOG_ERROR
            Debug.LogError($"[PFC_Error]:<color=#ffffff>{content}</color>"); 
#endif
        }
        
        public static void Error(object content,Color color)
        {
#if PFC_LOG_ERROR
            Debug.LogError($"[PFC_Error]:<color=#{ColorUtility.ToHtmlStringRGB(color)}>{content}</color>"); 
#endif
        }
    }
}