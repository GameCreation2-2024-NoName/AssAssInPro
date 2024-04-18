using UnityEngine.Events;

namespace PurpleFlowerCore
{
    public class ActionNode: IProcessNode
    {
        private readonly UnityAction _action;
        
        public ActionNode(UnityAction action)
        {
            _action = action;
        }

        public bool Update(float deltaTime)
        {
            
            _action?.Invoke();
            return true;
        }

        public void ReSet()
        {
            
        }
        
        public static implicit operator ActionNode(UnityAction action)
        {
            return new ActionNode(action);
        }
    }
}