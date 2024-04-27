using System;

namespace Pditine.Collide.CollideEvent
{
    public abstract class CollidingEventBase
    {
        public abstract bool CompareTag(string tag1, string tag2);
        public abstract void Happen(ColliderBase collider1,ColliderBase collider2);
    }
}