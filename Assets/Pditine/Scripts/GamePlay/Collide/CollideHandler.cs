namespace Pditine.Collide
{
    public static class CollideHandler
    {
        public static void ColliderHandle(CollideInfo info)
        {
            info.OnCollideEvent();
            
            info.TryDo();
        }
    }
}