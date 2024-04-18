using UnityEngine.Events;

namespace PurpleFlowerCore
{
    public interface IProcessNode
    {
        public bool Update(float deltaTime);

        public void ReSet();
    }
}