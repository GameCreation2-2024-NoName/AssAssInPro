using System;

namespace Pditine.Collide.CollideEvent
{
    public abstract class CollidingEventBase
    {
        public virtual int Priority => 0;
        protected abstract bool CompareTag(string tag1, string tag2);
        protected abstract void Happen(ColliderBase collider1,ColliderBase collider2,CollideInfo info);

        public virtual bool TryDo(CollideInfo info)
        {
            if (CompareTag(info.collider1.ColliderTag, info.collider2.ColliderTag))
            {
                Happen(info.collider1,info.collider2,info);
                return true;
            }

            if (CompareTag(info.collider2.ColliderTag, info.collider1.ColliderTag))
            {
                Happen(info.collider2,info.collider1,info);
                return true;
            }
            return false;
        }
    }
}