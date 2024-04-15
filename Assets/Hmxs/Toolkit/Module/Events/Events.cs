using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hmxs.Toolkit.Module.Events
{
    /// <summary>
    /// 事件中心，最多支持6个参数
    /// (事件通过字符串进行索引，请尽量使用EventGroups类)
    /// </summary>
    public static class Events
    {
        private static readonly Dictionary<string, IEventInfo> EventInfos = new();

        #region Internal interface/class

        private interface IEventInfo { }
        
        private class EventInfo : IEventInfo
        {
            public Action Actions;
        }
        
        private class EventInfo<T> : IEventInfo
        {
            public Action<T> Actions;
        }
        
        private class EventInfo<T0, T1> : IEventInfo
        {
            public Action<T0, T1> Action;
        }
        
        private class EventInfo<T0, T1, T2> : IEventInfo
        {
            public Action<T0, T1, T2> Action;
        }
        
        private class EventInfo<T0, T1, T2, T3> : IEventInfo
        {
            public Action<T0, T1, T2, T3> Action;
        }
        
        private class EventInfo<T0, T1, T2, T3, T4> : IEventInfo
        {
            public Action<T0, T1, T2, T3, T4> Action;
        }
        
        private class EventInfo<T0, T1, T2, T3, T4, T5> : IEventInfo
        {
            public Action<T0, T1, T2, T3, T4, T5> Action;
        }

        #endregion

        #region AddListener Methods
        /// <summary>
        /// 为事件添加监听
        /// T 为参数类型
        /// </summary>
        public static void AddListener(string eventName, Action action)
        {
            EventInfos.TryAdd(eventName, new EventInfo());
            ((EventInfo)EventInfos[eventName]).Actions += action;
        }

        public static void AddListener<T>(string eventName, Action<T> action)
        {
            EventInfos.TryAdd(eventName, new EventInfo<T>());
            ((EventInfo<T>)EventInfos[eventName]).Actions += action;
        }

        public static void AddListener<T0, T1>(string eventName, Action<T0, T1> action)
        {
            EventInfos.TryAdd(eventName, new EventInfo<T0, T1>());
            ((EventInfo<T0, T1>)EventInfos[eventName]).Action += action;
        }
        
        public static void AddListener<T0, T1, T2>(string eventName, Action<T0, T1, T2> action)
        {
            EventInfos.TryAdd(eventName, new EventInfo<T0, T1, T2>());
            ((EventInfo<T0, T1, T2>)EventInfos[eventName]).Action += action;
        }
        
        public static void AddListener<T0, T1, T2, T3>(string eventName, Action<T0, T1, T2, T3> action)
        {
            EventInfos.TryAdd(eventName, new EventInfo<T0, T1, T2, T3>());
            ((EventInfo<T0, T1, T2, T3>)EventInfos[eventName]).Action += action;
        }
        
        public static void AddListener<T0, T1, T2, T3, T4>(string eventName, Action<T0, T1, T2, T3, T4> action)
        {
            EventInfos.TryAdd(eventName, new EventInfo<T0, T1, T2, T3, T4>());
            ((EventInfo<T0, T1, T2, T3, T4>)EventInfos[eventName]).Action += action;
        }
        
        public static void AddListener<T0, T1, T2, T3, T4, T5>(string eventName, Action<T0, T1, T2, T3, T4, T5> action)
        {
            EventInfos.TryAdd(eventName, new EventInfo<T0, T1, T2, T3, T4, T5>());
            ((EventInfo<T0, T1, T2, T3, T4, T5>)EventInfos[eventName]).Action += action;
        }

        #endregion

        #region RemoveListener Methods
        /// <summary>
        /// 为事件移除监听
        /// T 为参数类型
        /// </summary>
        public static void RemoveListener(string eventName, Action action)
        {
            if (EventInfos.TryGetValue(eventName, out var value)) 
                ((EventInfo)value).Actions -= action;
        }
        
        public static void RemoveListener<T>(string eventName, Action<T> action)
        {
            if (EventInfos.TryGetValue(eventName, out var value)) 
                ((EventInfo<T>)value).Actions -= action;
        }
        
        public static void RemoveListener<T0, T1>(string eventName, Action<T0, T1> action)
        {
            if (EventInfos.TryGetValue(eventName, out var value)) 
                ((EventInfo<T0, T1>)value).Action -= action;
        }
        
        public static void RemoveListener<T0, T1, T2>(string eventName, Action<T0, T1, T2> action)
        {
            if (EventInfos.TryGetValue(eventName, out var value)) 
                ((EventInfo<T0, T1, T2>)value).Action -= action;
        }
        
        public static void RemoveListener<T0, T1, T2, T3>(string eventName, Action<T0, T1, T2, T3> action)
        {
            if (EventInfos.TryGetValue(eventName, out var value)) 
                ((EventInfo<T0, T1, T2, T3>)value).Action -= action;
        }
        
        public static void RemoveListener<T0, T1, T2, T3, T4>(string eventName, Action<T0, T1, T2, T3, T4> action)
        {
            if (EventInfos.TryGetValue(eventName, out var value)) 
                ((EventInfo<T0, T1, T2, T3, T4>)value).Action -= action;
        }
        
        public static void RemoveListener<T0, T1, T2, T3, T4, T5>(string eventName, Action<T0, T1, T2, T3, T4, T5> action)
        {
            if (EventInfos.TryGetValue(eventName, out var value)) 
                ((EventInfo<T0, T1, T2, T3, T4, T5>)value).Action -= action;
        }

        #endregion

        #region Trigger Methods
        /// <summary>
        /// 触发事件
        /// T 为参数类型
        /// </summary>
        public static void Trigger(string eventName)
        {
            if (EventInfos.TryGetValue(eventName, out var value)) 
                ((EventInfo)value).Actions?.Invoke();
            else
                Debug.LogWarning($"EventCenter: Null {eventName} is listened");
        }

        public static void Trigger<T>(string eventName, T param)
        {
            if (EventInfos.TryGetValue(eventName, out var value)) 
                ((EventInfo<T>)value).Actions?.Invoke(param);
            else
                Debug.LogWarning($"EventCenter: Null {eventName} is listened");
        }
        
        public static void Trigger<T0, T1>(string eventName, T0 param0, T1 param1)
        {
            if (EventInfos.TryGetValue(eventName, out var value)) 
                ((EventInfo<T0, T1>)value).Action?.Invoke(param0, param1);
            else
                Debug.LogWarning($"EventCenter: Null {eventName} is listened");
        }
        
        public static void Trigger<T0, T1, T2>(string eventName, T0 param0, T1 param1, T2 param2)
        {
            if (EventInfos.TryGetValue(eventName, out var value)) 
                ((EventInfo<T0, T1, T2>)value).Action?.Invoke(param0, param1, param2);
            else
                Debug.LogWarning($"EventCenter: Null {eventName} is listened");
        }
        
        public static void Trigger<T0, T1, T2, T3>(string eventName, T0 param0, T1 param1, T2 param2, T3 param3)
        {
            if (EventInfos.TryGetValue(eventName, out var value)) 
                ((EventInfo<T0, T1, T2, T3>)value).Action?.Invoke(param0, param1, param2, param3);
            else
                Debug.LogWarning($"EventCenter: Null {eventName} is listened");
        }
        
        public static void Trigger<T0, T1, T2, T3, T4>(string eventName, T0 param0, T1 param1, T2 param2, T3 param3, T4 param4)
        {
            if (EventInfos.TryGetValue(eventName, out var value)) 
                ((EventInfo<T0, T1, T2, T3, T4>)value).Action?.Invoke(param0, param1, param2, param3, param4);
            else
                Debug.LogWarning($"EventCenter: Null {eventName} is listened");
        }
        
        public static void Trigger<T0, T1, T2, T3, T4, T5>(string eventName, T0 param0, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
        {
            if (EventInfos.TryGetValue(eventName, out var value)) 
                ((EventInfo<T0, T1, T2, T3, T4, T5>)value).Action?.Invoke(param0, param1, param2, param3, param4, param5);
            else
                Debug.LogWarning($"EventCenter: Null {eventName} is listened");
        }
        
        #endregion

        #region DeleteEvent/Clear

        /// <summary>
        /// 删除某一事件
        /// </summary>
        public static bool DeleteEvent(string eventName) => EventInfos.Remove(eventName);

        /// <summary>
        /// 清空事件中心
        /// </summary>
        public static void Clear() => EventInfos.Clear();

        #endregion
    }
}