using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Hmxs.Toolkit.Flow.Timer
{
    /// <summary>
    /// 计时器，基于Update
    /// </summary>
    [Serializable]
    public class Timer
    {
        #region Public Properties

        /// <summary>
        /// 计时器ID
        /// </summary>
        public string Id { get; }
        
        /// <summary>
        /// 一轮计时会持续的时间
        /// </summary>
        public float Duration { get; }

        /// <summary>
        /// 是否会循环计时
        /// </summary>
        public bool IsLooped { get; set; }
        
        /// <summary>
        /// 计时是否会被timeScale影响
        /// </summary>
        public bool UseRealTime { get; }

        /// <summary>
        /// 是否已计时完成（只会在正常计时完成时被设置为true）
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        /// 是否正在暂停
        /// </summary>
        public bool IsPaused => _timePassedBeforePause.HasValue;
        
        /// <summary>
        /// 是否已被移除
        /// </summary>
        public bool IsRemoved => _timePassedBeforeRemove.HasValue;
        
        /// <summary>
        /// 是否已结束计时（在正常计时完成/被移除/拥有者已销毁时被设置为true）
        /// </summary>
        public bool IsOver => IsCompleted || IsRemoved || IsOwnerDestroyed;
        
        /// <summary>
        /// 一轮计时已经过时间（不包括暂停、取消期间的时长）
        /// </summary>
        public float TimePassed => GetPassedTime();

        /// <summary>
        /// 一轮计时剩余时间
        /// </summary>
        public float TimeRemaining => Duration - GetPassedTime();

        /// <summary>
        /// 一轮计时已过时间比例
        /// </summary>
        public float RatioPassed => GetPassedTime() / Duration;
        
        /// <summary>
        /// 一轮计时剩余时间比例
        /// </summary>
        public float RatioRemaining => (Duration - GetPassedTime()) / Duration;
        
        #endregion

        #region Private Field
        
        private readonly Action _onComplete;                            // 一轮计时完成回调
        private readonly Action<float> _onUpdate;                       // 每帧计时回调
        private readonly MonoBehaviour _owner;                          // 计时器拥有者
        private readonly bool _hasOwner;                                // 计时器是否有拥有者
        private bool IsOwnerDestroyed => _hasOwner && _owner == null;   // 计时器有拥有者且拥有者被销毁
        private float _startTime;                                       // 一轮计时开始的时间
        private float _lastUpdateTime;                                  // 上一轮计时的时间，用于计算时间间隔
        private float? _timePassedBeforePause;                          // Nullable<float>，表示暂停之前此轮计时已经持续的时间，是否为空表示暂停状态
        private float? _timePassedBeforeRemove;                         // Nullable<float>，表示移除之前此轮计时已经持续的时间，是否为空表示移除状态

        private static TimersManager _manager;

        private static TimersManager Manager                    // 若TimerManager为空，尝试通过场景查找获取，若依然为空则创建一个TimerManager
        {
            get
            {
                if (_manager != null) return _manager;

                _manager = Object.FindObjectOfType<TimersManager>();
                if (_manager != null) return _manager;

                _manager = new GameObject("TimerManager").AddComponent<TimersManager>();
                return _manager;
            }
        }

        #endregion
        
        #region Static Method（API）

        /// <summary>
        /// 注册一个计时器，会通过私有构造函数实例化一个Timer，会因场景切换而被摧毁
        /// </summary>
        /// ///
        /// <param name="duration">一轮计时会持续的时间</param>
        /// <param name="onComplete">一轮计时完成回调</param>
        /// <param name="onUpdate">每帧计时回调</param>
        /// <param name="owner">计时器拥有者，当拥有者被销毁时，计时器会自动移除，防止<see cref="NullReferenceException"/>错误</param>
        /// <param name="isLooped">是否会循环计时</param>
        /// <param name="useRealTime">计时是否会被timeScale影响</param>
        /// <param name="timerID">计时器ID</param>
        /// <returns>Timer对象</returns>
        public static Timer Register(
            float duration,
            Action onComplete,
            Action<float> onUpdate = null,
            MonoBehaviour owner = null,
            bool isLooped = false,
            bool useRealTime = false,
            string timerID = "Default Timer")
        {
            var timer = new Timer(duration, onComplete, onUpdate, isLooped, useRealTime, owner,timerID);
            if (Manager == null)
            {
                Debug.Log("Can not find TimerUpdater");
                return null;
            }
            Manager.Add(timer);
            return timer;
        }
        
        /// <summary>
        /// 获得某一ID的全部计时器
        /// </summary>
        /// <param name="timerID">计时器ID</param>
        public static List<Timer> GetById(string timerID) => Manager.Get(timerID);
        
        /// <summary>
        /// 暂停某一计时器(自动判空)
        /// </summary>
        public static void Pause(Timer timer) => timer?.Pause();

        /// <summary>
        /// 解除某一计时器的暂停(自动判空)
        /// </summary>
        public static void UnPause(Timer timer) => timer?.UnPause();

        /// <summary>
        /// 移除某一计时器(自动判空)
        /// </summary>
        public static void Remove(Timer timer) => timer?.Remove();

        /// <summary>
        /// 移除某一ID的全部计时器
        /// </summary>
        public static void Remove(string timerID)
        {
            foreach (var timer in GetById(timerID)) timer?.Remove();
        }

        /// <summary>
        /// 清除所有计时器(自动判空)
        /// </summary>
        public static void Clear() => Manager.ClearAll();

        #endregion
        
        #region Public Method(API)

        /// <summary>
        /// 暂停计时器(若计时器为null会报错)
        /// </summary>
        public void Pause()
        {
            if (IsPaused || IsOver) return;
            _timePassedBeforePause = GetPassedTime();
        }

        /// <summary>
        /// 解除计时器暂停状态(若计时器为null会报错)
        /// </summary>
        public void UnPause()
        {
            if (!IsPaused || IsOver) return;
            _timePassedBeforePause = null;
        }
        
        /// <summary>
        /// 移除计时器(若计时器为null会报错)
        /// </summary>
        public void Remove()
        {
            if (IsOver) return;
            _timePassedBeforeRemove = GetPassedTime();
            _timePassedBeforePause = null;
        }
        
        #endregion

        #region Private Method

        // 私有构造函数，初始化Timer
        private Timer(
            float duration,
            Action onComplete,
            Action<float> onUpdate,
            bool isLooped,
            bool useRealTime,
            MonoBehaviour owner,
            string timerID)
        {
            Duration = duration;
            _onComplete = onComplete;
            _onUpdate = onUpdate;
            IsLooped = isLooped;
            UseRealTime = useRealTime;
            _owner = owner;
            _hasOwner = owner != null;
            _startTime = GetCurrentTime();
            _lastUpdateTime = _startTime;
            Id = timerID;
        }

        // 每帧调用，更新计时器状态
        private void Update()
        {
            if (IsOver) return;

            if (IsPaused)
            {
                _startTime += GetDeltaTime();
                _lastUpdateTime = GetCurrentTime();
                return;
            }
            
            _lastUpdateTime = GetCurrentTime();

            _onUpdate?.Invoke(GetPassedTime());
            
            if (!(GetCurrentTime() >= GetEndTime())) return;

            _onComplete?.Invoke();

            if (IsLooped)
                _startTime = GetCurrentTime();
            else
                IsCompleted = true;
        }

        private float GetCurrentTime() => UseRealTime ? Time.realtimeSinceStartup : Time.time;
        
        private float GetEndTime() => _startTime + Duration;

        private float GetDeltaTime() => GetCurrentTime() - _lastUpdateTime;

        private float GetPassedTime()
        {
            if (IsCompleted || GetCurrentTime() >= GetEndTime())
                return Duration;
            return _timePassedBeforeRemove ?? _timePassedBeforePause ?? GetCurrentTime() - _startTime;
        }
        
        #endregion

        #region Private Class
        
        // 计时器管理器，用于管理所有计时器
        private class TimersManager : MonoBehaviour
        {
            [SerializeField] [ReadOnly] private List<Timer> timers = new();
            [ReadOnly] private readonly List<Timer> _timersBuffer = new();

            private void Update()
            {
                if (_timersBuffer.Count > 0)
                {
                    timers.AddRange(_timersBuffer);
                    _timersBuffer.Clear();
                }

                foreach (var timer in timers)
                {
                    timer.Update();
#if UNITY_EDITOR
                    timer.UpdateInfo();
#endif
                }

                timers.RemoveAll(timer => timer.IsOver);
            }
            
            public void Add(Timer timer) => _timersBuffer.Add(timer);

            public List<Timer> Get(string id) => timers.FindAll(timer => timer.Id == id);

            public void ClearAll()
            {
                foreach (var timer in timers) 
                    timer?.Remove();
                timers.Clear();
                _timersBuffer.Clear();
            }
        }
        
        #endregion
        
        #region SerializeHelper

#if UNITY_EDITOR
        [SerializeField] private string id;
        [SerializeField] private float duration;
        [SerializeField] private bool useReadTime;
        [SerializeField] private bool isLoop;
        [SerializeField] private bool isPause;
        [SerializeField] private float timePassed;
        [SerializeField] [ProgressBar(0f,1f)] private float ratioPassed;

        private void UpdateInfo()
        {
            id = Id;
            duration = Duration;
            useReadTime = UseRealTime;
            isLoop = IsLooped;
            isPause = IsPaused;
            timePassed = TimePassed;
            ratioPassed = RatioPassed;
        }
#endif

        #endregion
    }
}