namespace Pditine.Collide
{
    public static class CollideHandler
    {
        public static void ColliderHandle(string tag1,string tag2, ColliderBase collider1,ColliderBase collider2)
        {
            collider1.CallBack?.Invoke();
            collider2.CallBack?.Invoke();
            
            if(collider1.Events is not null)
                foreach (var theEvent in collider1.Events)
                {
                    if(theEvent.TryDo(tag1, tag2, collider1, collider2))
                        return;
                }
            if(collider2.Events is not null)
                foreach (var theEvent in collider2.Events)
                {
                    if(theEvent.TryDo(tag1, tag2, collider1, collider2))
                        return;
                }
        }
    }
}