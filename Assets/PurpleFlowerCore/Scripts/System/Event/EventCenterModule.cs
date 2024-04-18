using System.Collections.Generic;
using UnityEngine.Events;

namespace PurpleFlowerCore.Event
{
    public class EventCenterModule
    {
        private Dictionary<string, EventDataBase> _events = new();

        #region 触发事件

        public void EventTrigger(string eventName)
        {
            if (_events.TryGetValue(eventName, out var value))
            {
                (value as EventData)?.Action?.Invoke();
            }
        }
        
        public void EventTrigger<T0>(string eventName,T0 info0)
        {
            if (_events.TryGetValue(eventName, out var value))
            {
                (value as EventData<T0>)?.Action?.Invoke(info0);
            }
        }
        
        public void EventTrigger<T0,T1>(string eventName,T0 info0,T1 info1)
        {
            if (_events.TryGetValue(eventName, out var value))
            {
                (value as EventData<T0,T1>)?.Action?.Invoke(info0,info1);
            }
        }

        #endregion

        #region 添加事件监听

        public void AddEventListener(string eventName,UnityAction action)
        {
            if (_events.TryGetValue(eventName, value: out var @event))
            {
                (@event as EventData).Action += action;
                return;
            }
            _events.Add(eventName,new EventData(action));
        }
        
        public void AddEventListener<T0>(string eventName,UnityAction<T0> action)
        {
            if (_events.TryGetValue(eventName, value: out var @event))
            {
                (@event as EventData<T0>).Action += action;
                return;
            }
            _events.Add(eventName,new EventData<T0>(action));
        }
        
        public void AddEventListener<T0,T1>(string eventName,UnityAction<T0,T1> action)
        {
            if (_events.TryGetValue(eventName, value: out var @event))
            {
                (@event as EventData<T0,T1>).Action += action;
                return;
            }
            _events.Add(eventName,new EventData<T0,T1>(action));
        }

        #endregion

        #region 移除事件监听

        public void RemoveEventListener(string eventName,UnityAction action)
        {
            if (_events.TryGetValue(eventName, value: out var @event))
                (@event as EventData).Action -= action;
        }
        
        public void RemoveEventListener<T0>(string eventName,UnityAction<T0> action)
        {
            if (_events.TryGetValue(eventName, value: out var @event))
                (@event as EventData<T0>).Action -= action;
        }
        
        public void RemoveEventListener<T0,T1>(string eventName,UnityAction<T0,T1> action)
        {
            if (_events.TryGetValue(eventName, value: out var @event))
                (@event as EventData<T0,T1>).Action -= action;
        }

        #endregion

        public void Clear()
        {
            _events.Clear();
        }

        public void Clear(string eventName)
        {
            if (_events.ContainsKey(eventName))
                _events.Remove(eventName);
        }
    }
}