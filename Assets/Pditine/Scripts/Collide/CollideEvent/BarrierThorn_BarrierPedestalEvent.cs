using Pditine.Audio;
using Pditine.Map;

namespace Pditine.Collide.CollideEvent
{
    public class BarrierThorn_BarrierPedestalEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "BarrierThorn" && tag2 == "BarrierPedestal") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2)
        {
            var theBarrier1 = (collider1 as BarrierThorn).TheBarrier;
            var theBarrier2 = (collider2 as BarrierPedestal).TheBarrier;
            
            // (collider1 as BarrierThorn).TheBarrier.ThePedestal.AddCollider(collider2);
            // collider2.AddCollider((collider1 as BarrierThorn).TheBarrier.ThePedestal);
            //
            // PFCLog.Info((collider2 as BarrierPedestal).TheBarrier.TheThorn);
            // PFCLog.Info(collider1);
            // (collider2 as BarrierPedestal).TheBarrier.TheThorn.AddCollider((collider1));
            //
            // collider1.AddCollider((collider2 as BarrierThorn).TheBarrier.TheThorn);
            
            (theBarrier1.Direction, theBarrier2.Direction) = (theBarrier2.Direction, theBarrier1.Direction);
            (theBarrier1.CurrentSpeed, theBarrier2.CurrentSpeed) = (theBarrier2.CurrentSpeed, theBarrier1.CurrentSpeed);
            theBarrier1.HitFeedback.PlayFeedbacks();
            theBarrier2.HitFeedback.PlayFeedbacks();
            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
        }
    }
}