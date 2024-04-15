using System;
using System.Collections;

namespace Hmxs.Toolkit.Flow.CoroutineTask
{
    public static class CoroutineTaskExtension
    {
        #region Extension Method
        
        /// <summary>
        /// 将协程函数构建为Task并启动
        /// </summary>
        /// <param name="enumerator">返回值为IEnumerator的函数</param>
        /// <param name="callback">协程结束回调</param>
        /// <returns>Task</returns>
        public static CoroutineTask StartAsTask(this IEnumerator enumerator, Action<bool> callback = null)
        {
            var task = new Flow.CoroutineTask.CoroutineTask(enumerator);
            if (callback != null) task.OnComplete += callback;
            task.Start();
            return task;
        }

        #endregion
    }
}