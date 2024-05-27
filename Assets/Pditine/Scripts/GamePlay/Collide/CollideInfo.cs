using UnityEngine;

namespace Pditine.Collide
{
    public class CollideInfo
    {
        public string tag1;
        public string tag2;
        public ColliderBase collider1;
        public ColliderBase collider2;
        public Collision2D Collision2D;

        public CollideInfo(string tag1, string tag2, ColliderBase collider1, ColliderBase collider2,Collision2D Collision2D)
        {
            this.tag1 = tag1;
            this.tag2 = tag2;
            this.collider1 = collider1;
            this.collider2 = collider2;
            this.Collision2D = Collision2D;
        }


        public void OnCollideEvent()
        {
            collider1.OnCollide?.Invoke(collider2);
            collider2.OnCollide?.Invoke(collider1);
        }

        public void TryDo()
        {
            if(collider1.Events is not null)
                foreach (var theEvent in collider1.Events)
                {
                    if(theEvent.TryDo(this))
                        return;
                }
            if(collider2.Events is not null)
                foreach (var theEvent in collider2.Events)
                {
                    if(theEvent.TryDo(this))
                        return;
                }
        }
    }
}