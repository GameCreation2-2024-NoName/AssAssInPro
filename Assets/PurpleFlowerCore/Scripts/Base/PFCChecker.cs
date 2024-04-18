using System.IO;
using UnityEngine;

namespace PurpleFlowerCore.Base
{
    public static class PFCChecker
    {
        /// <summary>
        /// 用于检测该场景是否存在PurpleFlowerCore预制体
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        private static void CheckPurpleFlowerCore()
        {
            // if (!Object.FindObjectOfType<PFCManager>())
            //     Debug.LogWarning("[该场景没有PurpleFlowerCore,某些功能不可使用]\n" +
            //                      "[This scene doesn't have the GameObject,PurpleFlowerCore,some function given by that may not work]");
            if (!Directory.Exists(Application.dataPath + "/" + "PurPleFlowerCore"))
                Debug.LogWarning("[请将PurpleFlowerCore放置于根目录]\n" +
                                  "[Place PurpleFlowerCore in the root directory]");
        }
    }
}