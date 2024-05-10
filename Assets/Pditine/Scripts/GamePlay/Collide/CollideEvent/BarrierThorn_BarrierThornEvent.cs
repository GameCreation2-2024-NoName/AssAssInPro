using Pditine.Audio;
using Pditine.Map;
using Pditine.Utility;

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
            
            var res =
                PhysicsUtility.ElasticCollision(theBarrier1.Direction * theBarrier1.CurrentSpeed,
                    theBarrier2.Direction * theBarrier2.CurrentSpeed,
                    theBarrier1.Weight, theBarrier2.Weight, theBarrier1.transform.position, theBarrier2.transform.position);
            theBarrier1.Direction = res.v1Prime.normalized;
            theBarrier2.Direction = res.v2Prime.normalized;
            theBarrier1.CurrentSpeed = res.v1Prime.magnitude;
            theBarrier2.CurrentSpeed = res.v2Prime.magnitude;
            
            theBarrier1.HitFeedback.PlayFeedbacks();
            theBarrier2.HitFeedback.PlayFeedbacks();
            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
        }
    }
}