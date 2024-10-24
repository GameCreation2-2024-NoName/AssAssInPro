using Pditine.Audio;
using Pditine.Map;
using Pditine.Player.Thorn;
using Pditine.Utility;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.Collide.CollideEvent
{
    public class CrossButtonl_ThornEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "CrossButton" && tag2 == "Thorn") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2,CollideInfo info)
        {
            var thePlayer = (collider2 as ThornBase).ThePlayer;
            var originDirection = thePlayer.CurrentDirection;
            var normal = info.Collision2D.contacts[0].normal;
            Vector2 Out_Direction = Vector2.Reflect(originDirection,normal);
            thePlayer.CurrentDirection = Out_Direction;
            (Vector3,Vector2) feedBackData = (info.Collision2D.contacts[0].point, 
                PhysicsUtility.SparkDir(originDirection,normal,thePlayer.CurrentSpeed * 5));
            thePlayer.VFX.Play("Hit",feedBackData);
            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
            
            (collider1 as CrossButton).TryTrigger();
        }
    }
}