using Pditine.Audio;
using Pditine.Map;
using Pditine.Player.Thorn;
using Pditine.Utility;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.Collide.CollideEvent
{
    public class BarrierPedestal_ThornEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "BarrierPedestal" && tag2 == "Thorn") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2,CollideInfo info)
        {
                var thePlayer = (collider2 as ThornBase).ThePlayer;
                var theBarrier = (collider1 as BarrierPedestal).TheBarrier; 
                
                (collider1 as BarrierPedestal).TheBarrier.TheThorn.AddCollider(collider2);
                collider2.AddCollider((collider1 as BarrierPedestal).TheBarrier.TheThorn);
                
                (Vector3,Vector2) feedBackData = (info.Collision2D.contacts[0].point, 
                    PhysicsUtility.SparkDir(thePlayer.CurrentDirection,theBarrier.Direction,thePlayer.CurrentSpeed * 5));
                thePlayer.VFX.Play("Hit",feedBackData);
                
                theBarrier.Direction = thePlayer.CurrentDirection;
                thePlayer.CurrentDirection = -thePlayer.CurrentDirection;
                theBarrier.CurrentSpeed = thePlayer.CurrentSpeed/1.5f;
                thePlayer.CurrentSpeed /= 1.2f;

                AAIAudioManager.Instance.PlayEffect("碰撞音效1");
        }
    }
}