using UnityEngine.Events;

namespace PurpleFlowerCore.Event
{
    public abstract class EventDataBase{}

    public class EventData : EventDataBase
    {
        public UnityAction Action;

        public EventData(UnityAction action)
        {
            Action = action;
        }
        
    }
    
    public class EventData<T0> : EventDataBase
    {
        public UnityAction<T0> Action;

        public EventData(UnityAction<T0> action)
        {
            Action = action;
        }
        
    }
    
    
    public class EventData<T0,T1> : EventDataBase
    {
        public UnityAction<T0,T1> Action;

        public EventData(UnityAction<T0,T1> action)
        {
            Action = action;
        }
        
    }
}