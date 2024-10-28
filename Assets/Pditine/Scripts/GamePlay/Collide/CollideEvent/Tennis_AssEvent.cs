using Pditine.Audio;
using Pditine.Map;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
using Pditine.Utility;
using UnityEngine;

namespace Pditine.Collide.CollideEvent
{
    public class Tennis_AssEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "Tennis" && tag2 == "Ass") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2,CollideInfo info)
        {
            var theTennis = collider1 as Tennis;
            var thePlayer = (collider2 as AssBase).ThePlayer;
            
            var res =
                PhysicsUtility.ElasticCollision(theTennis.direction * theTennis.currentSpeed,
                    thePlayer.CurrentDirection * thePlayer.CurrentSpeed,
                    thePlayer.Weight, theTennis.Weight, thePlayer.transform.position, theTennis.transform.position);
            
            (Vector3,Vector2) feedBackData = (info.Collision2D.contacts[0].point, 
                PhysicsUtility.SparkDir(thePlayer.CurrentDirection,theTennis.direction,thePlayer.CurrentSpeed * 5));
            thePlayer.VFX.Play("Hit",feedBackData);
            
            theTennis.direction = res.v1Prime.normalized;
            theTennis.direction = new Vector3(0, theTennis.direction.y, 0);
            thePlayer.CurrentDirection = res.v2Prime.normalized;
            theTennis.currentSpeed = res.v1Prime.magnitude;
            thePlayer.CurrentSpeed = res.v2Prime.magnitude;
            
            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
        }
    }
}