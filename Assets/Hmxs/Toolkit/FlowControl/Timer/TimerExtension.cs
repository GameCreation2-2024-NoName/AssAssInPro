using System;
using UnityEngine;

namespace Hmxs.Toolkit.Flow.Timer
{
    public static class TimerExtension
    {
        #region Extensions Method

        /// <summary>
        /// 注册一个附加于Mono的计时器，会通过私有构造函数实例化一个Timer，会随Mono的生命周期而销毁
        /// </summary>
        /// <param name="behaviour"></param>
        /// <param name="duration">一轮计时会持续的时间</param>
        /// <param name="onComplete">一轮计时完成回调</param>
        /// <param name="onUpdate">每帧计时回调</param>
        /// <param name="isLooped">是否会循环计时</param>
        /// <param name="useRealTime">计时是否会被timeScale影响</param>
        /// <param name="timerID">计时器ID</param>
        /// <returns>Timer对象</returns>
        public static Timer AttachTimer(this MonoBehaviour behaviour, float duration, Action onComplete, 
            Action<float> onUpdate = null, bool isLooped = false, bool useRealTime = false, string timerID = "Default Timer")
        {
            return Timer.Register(duration, onComplete, onUpdate, behaviour, isLooped, useRealTime, timerID);
        }

        #endregion
    }
}