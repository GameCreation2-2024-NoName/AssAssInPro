using UnityEngine.Events;
using PurpleFlowerCore.Event;
namespace PurpleFlowerCore
{
    public static class EventSystem
    {
        private static EventCenterModule _eventCenter;

        private static EventCenterModule EventCenter
        {
            get
            {
                if (_eventCenter is null)
                    _eventCenter = new EventCenterModule();
                return _eventCenter;
            }
        }
        
        #region 触发事件

        public static void EventTrigger(string eventName)
        {
            EventCenter.EventTrigger(eventName);
        }
        
        public static void EventTrigger<T0>(string eventName,T0 info0)
        {
            EventCenter.EventTrigger<T0>(eventName,info0);
        }
        
        public static void EventTrigger<T0,T1>(string eventName,T0 info0,T1 info1)
        {
            EventCenter.EventTrigger<T0,T1>(eventName,info0,info1);
        }

        #endregion

        #region 添加事件监听

        public static void AddEventListener(string eventName,UnityAction action)
        {
            EventCenter.AddEventListener(eventName,action);
        }
        
        public static void AddEventListener<T0>(string eventName,UnityAction<T0> action)
        {
            EventCenter.AddEventListener<T0>(eventName,action);
        }
        
        public static void AddEventListener<T0,T1>(string eventName,UnityAction<T0,T1> action)
        {
            EventCenter.AddEventListener<T0,T1>(eventName,action);
        }

        #endregion

        #region 移除事件监听

        public static void RemoveEventListener(string eventName,UnityAction action)
        {
            EventCenter.RemoveEventListener(eventName,action);
        }
        
        public static void RemoveEventListener<T0>(string eventName,UnityAction<T0> action)
        {
            EventCenter.RemoveEventListener<T0>(eventName,action);
        }
        
        public static void RemoveEventListener<T0,T1>(string eventName,UnityAction<T0,T1> action)
        {
            EventCenter.RemoveEventListener<T0,T1>(eventName,action);
        }

        #endregion

        public static void Clear()
        {
            EventCenter.Clear();
        }

        public static void Clear(string eventName)
        {
            EventCenter.Clear(eventName);
        }
    }
}