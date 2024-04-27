using Pditine.Map;

namespace Pditine.Collide.CollideEvent
{
    public class BarrierThorn_BarrierThornEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "BarrierThorn" && tag2 == "BarrierThorn") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2)
        {
            var theBarrier1 = (collider1 as BarrierThorn).TheBarrier;
            var theBarrier2 = (collider2 as BarrierThorn).TheBarrier;
            
            (collider1 as BarrierThorn).TheBarrier.ThePedestal.AddCollider(collider2);
            collider2.AddCollider((collider1 as BarrierThorn).TheBarrier.ThePedestal);
            (collider2 as BarrierThorn).TheBarrier.ThePedestal.AddCollider(collider1);
            collider1.AddCollider((collider2 as BarrierThorn).TheBarrier.ThePedestal);
            
            (theBarrier1.Direction, theBarrier2.Direction) = (theBarrier2.Direction, theBarrier1.Direction);
            (theBarrier1.CurrentSpeed, theBarrier2.CurrentSpeed) = (theBarrier2.CurrentSpeed, theBarrier1.CurrentSpeed);
            theBarrier1.HitFeedback.PlayFeedbacks();
            theBarrier2.HitFeedback.PlayFeedbacks();
        }
    }
}