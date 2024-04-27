using System;

namespace Pditine.Collide.CollideEvent
{
    public abstract class CollidingEventBase
    {
        protected abstract bool CompareTag(string tag1, string tag2);
        protected abstract void Happen(ColliderBase collider1,ColliderBase collider2);

        public virtual bool TryDo(string tag1, string tag2, ColliderBase collider1, ColliderBase collider2)
        {
            if (CompareTag(tag1, tag2))
            {
                Happen(collider1,collider2);
                return true;
            }

            if (CompareTag(tag2, tag1))
            {
                Happen(collider2,collider1);
                return true;
            }
            return false;
        }
    }
}