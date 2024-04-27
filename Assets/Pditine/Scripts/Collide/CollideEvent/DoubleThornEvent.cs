using System;

namespace Pditine.Collide.CollideEvent
{
    public class DoubleThornEvent : CollidingEventBase
    {
        public override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "Thorn" && tag2 == "Thorn") return true;
            return false;
        }
        
        public override void Happen(ColliderBase collider1, ColliderBase collider2)
        {
            throw new NotImplementedException();
        }
    }
}