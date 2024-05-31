using Pditine.Audio;
using Pditine.Map;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
using Pditine.Utility;
using UnityEngine;

namespace Pditine.Collide.CollideEvent
{
    public class Tennis_DynamicBarrierEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "Tennis" && tag2 == "DynamicBarrier") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2,CollideInfo info)
        {
            var theTennis = collider1 as Tennis;
            var theDynamicBarrier = collider2 as DynamicBarrier;
            
            var res =
                PhysicsUtility.ElasticCollision(theTennis.direction * theTennis.currentSpeed,
                    theDynamicBarrier.Direction * theDynamicBarrier.CurrentSpeed,
                    theDynamicBarrier.Weight, theTennis.Weight, theDynamicBarrier.transform.position, theTennis.transform.position);
            
            theTennis.direction = res.v1Prime.normalized;
            theTennis.direction = new Vector3(0, theTennis.direction.y, 0);
            theDynamicBarrier.Direction = res.v2Prime.normalized;
            theTennis.currentSpeed = res.v1Prime.magnitude;
            theDynamicBarrier.CurrentSpeed = res.v2Prime.magnitude;
            
            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
        }
    }
}