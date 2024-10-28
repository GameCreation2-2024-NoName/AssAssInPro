using Pditine.Audio;
using Pditine.Map;
using Pditine.Player.Thorn;
using Pditine.Utility;
using UnityEngine;

namespace Pditine.Collide.CollideEvent
{
    public class HollowCircle_ThornEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "HollowCircle" && tag2 == "Thorn") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2,CollideInfo info)
        {
            var thePlayer = (collider2 as ThornBase).ThePlayer;
            var theCircle = collider1 as HollowCircle; 
                
            var res =
                PhysicsUtility.ElasticCollision(thePlayer.CurrentDirection * thePlayer.CurrentSpeed,
                    theCircle.GetTangent(info.Collision2D.contacts[0].point) * theCircle.CalculatingSpeed,
                    thePlayer.Weight, theCircle.Weight, thePlayer.transform.position, theCircle.transform.position);
            
            (Vector3,Vector2) feedBackData = (info.Collision2D.contacts[0].point, 
                PhysicsUtility.SparkDir(thePlayer.CurrentDirection,theCircle.GetTangent(info.Collision2D.contacts[0].point) * theCircle.CalculatingSpeed,thePlayer.CurrentSpeed * 5));
            thePlayer.VFX.Play("Hit",feedBackData);
            
            thePlayer.CurrentDirection = res.v1Prime.normalized;
            thePlayer.CurrentSpeed = res.v1Prime.magnitude;
            
            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
        }
    }
}