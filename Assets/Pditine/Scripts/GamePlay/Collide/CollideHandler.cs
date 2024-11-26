using System.Collections;
using System.Collections.Generic;
using Pditine.Collide.CollideEvent;
using UnityEngine;

namespace Pditine.Collide
{
    public static class CollideHandler
    {
        private static readonly List<CollidingEventBase> CurrentEvents = new();
        public static void ColliderHandle(CollideInfo info)
        {
            var collider1 = info.collider1;
            var collider2 = info.collider2;
            collider1.OnCollide?.Invoke(collider2,info);
            collider2.OnCollide?.Invoke(collider1,info);

            CurrentEvents.Clear();
            CurrentEvents.AddRange(collider1.Events);
            CurrentEvents.AddRange(collider2.Events);
            CurrentEvents.Sort((a, b) => a.Priority.CompareTo(b.Priority));
            foreach (var theEvent in CurrentEvents)
            {
                if(theEvent.TryDo(info))
                    return;
            }
        }
    }
    public struct CollideInfo
    {
        public ColliderBase collider1;
        public ColliderBase collider2;
        public Collision2D Collision2D;

        public CollideInfo(ColliderBase collider1, ColliderBase collider2,Collision2D Collision2D)
        {
            this.collider1 = collider1;
            this.collider2 = collider2;
            this.Collision2D = Collision2D;
        }
    }
}