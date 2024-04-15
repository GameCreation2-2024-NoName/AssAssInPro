using System;
using System.Collections;
using Hmxs.Toolkit.Base.Singleton;

namespace Hmxs.Toolkit.Flow.CoroutineTask
{
    /// <summary>
    /// 协程控制器
    /// 通过CallWrapper()对协程函数进行包裹，使用迭代器的性质控制协程的运行
    /// </summary>
    public class CoroutineTask
    {
        #region Public Properties/Fields

        /// <summary>
        /// Task内包含的协程函数
        /// </summary>
        public IEnumerator Enumerator { get; }
        
        /// <summary>
        /// Task是否处于暂停状态
        /// </summary>
        public bool IsPaused { get; private set; }
        
        /// <summary>
        /// Task是否正在运行
        /// </summary>
        public bool IsRunning { get; private set; }
        
        /// <summary>
        /// Task是否被手动停止，如果协程正常运行结束则为false
        /// </summary>
        public bool IsStopped { get; private set; }
        
        /// <summary>
        /// 协程运行结束回调，当协程被手动停止时传入true，反之传入false
        /// </summary>
        public Action<bool> OnComplete;
        
        #endregion

        #region Public Method
        
        /// <summary>
        /// Task构造函数，构建完成后需要手动调用Start()函数启动Task
        /// 更建议使用IEnumerator的拓展方法StartAsTask()构建Task
        /// </summary>
        /// <param name="enumerator">返回值为IEnumerator的函数</param>
        /// <param name="callback">协程结束回调</param>
        public CoroutineTask(IEnumerator enumerator, Action<bool> callback = null)
        {
            Enumerator = enumerator;
            if (callback != null) OnComplete += callback;
        }

        /// <summary>
        /// 启动Task
        /// </summary>
        public void Start()
        {
            if (Enumerator == null) return;
            IsRunning = true;
            CoroutineTaskManager.Instance.StartTask(CallWrapper());
        }

        /// <summary>
        /// 暂停Task
        /// </summary>
        public void Pause() => IsPaused = true;
        
        /// <summary>
        /// 解除Task的暂停
        /// </summary>
        public void UnPause() => IsPaused = false;

        /// <summary>
        /// 停止Task
        /// </summary>
        public void Stop()
        {
            IsStopped = true;
            IsRunning = false;
        }
        
        #endregion

        #region Private

        private void Finish()
        {
            OnComplete?.Invoke(IsStopped);
            OnComplete = default;
        }

        /// <summary>
        /// 协程包装
        /// </summary>
        private IEnumerator CallWrapper()
        {
            yield return null;
            var task = Enumerator;
            while (IsRunning)
            {
                if (IsPaused)
                    yield return null;
                else
                {
                    if (task != null && task.MoveNext())
                        yield return task.Current;
                    else
                        IsRunning = false;
                }
            }
            Finish();
        }
        
        internal class CoroutineTaskManager : SingletonMono<CoroutineTaskManager>
        {
            public void StartTask(IEnumerator enumerator) => StartCoroutine(enumerator);
        }
        
        #endregion
    }
}