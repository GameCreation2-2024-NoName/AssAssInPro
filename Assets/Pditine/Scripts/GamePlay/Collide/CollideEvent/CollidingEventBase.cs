﻿using System;

namespace Pditine.Collide.CollideEvent
{
    public abstract class CollidingEventBase
    {
        protected abstract bool CompareTag(string tag1, string tag2);
        protected abstract void Happen(ColliderBase collider1,ColliderBase collider2,CollideInfo info);

        public virtual bool TryDo(CollideInfo info)
        {
            if (CompareTag(info.tag1, info.tag2))
            {
                Happen(info.collider1,info.collider2,info);
                return true;
            }

            if (CompareTag(info.tag2, info.tag1))
            {
                Happen(info.collider2,info.collider1,info);
                return true;
            }
            return false;
        }
    }
}