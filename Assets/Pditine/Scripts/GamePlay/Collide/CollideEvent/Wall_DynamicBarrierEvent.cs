using Pditine.Audio;
using Pditine.Map;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
using Pditine.Utility;
using UnityEngine;

namespace Pditine.Collide.CollideEvent
{
    public class Wall_DynamicBarrierEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "Wall" && tag2 == "DynamicBarrier") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2,CollideInfo info)
        {
            //Debug.Log(1);
            var theBarrier= collider2 as DynamicBarrier;
            var originDirection = theBarrier.Direction;
            Vector2 Out_Direction = Vector2.Reflect(originDirection,info.Collision2D.contacts[0].normal);
            theBarrier.Direction = Out_Direction;
            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
        }
    }
}