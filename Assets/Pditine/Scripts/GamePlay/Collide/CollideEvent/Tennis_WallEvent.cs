using Pditine.Audio;
using Pditine.Map;
using Pditine.Player.Thorn;
using Pditine.Utility;
using UnityEngine;

namespace Pditine.Collide.CollideEvent
{
    public class Tennis_WallEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "Tennis" && tag2 == "Wall") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2,CollideInfo info)
        {
            var theTennis = collider1 as Tennis;
            var theWall = collider2 as Wall;
            
            var originDirection = theTennis.direction;
            Vector2 outDirection = Vector2.Reflect(originDirection,info.Collision2D.contacts[0].normal);
            theTennis.direction = outDirection;
            
            //AAIAudioManager.Instance.PlayEffect("碰撞音效1");
        }
    }
}