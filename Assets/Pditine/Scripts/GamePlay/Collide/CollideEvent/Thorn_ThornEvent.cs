using Pditine.Audio;
using Pditine.Player.Thorn;
using Pditine.Utility;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.Collide.CollideEvent
{
    public class Thorn_ThornEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "Thorn" && tag2 == "Thorn") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2,CollideInfo info)
        {
            var thePlayer1 = (collider1 as ThornBase).ThePlayer;
            var thePlayer2 = (collider2 as ThornBase).ThePlayer;
            
            thePlayer1.TheThorn.AddCollider(thePlayer2.TheAss);
            thePlayer2.TheThorn.AddCollider(thePlayer1.TheAss);
            
            var res =
            PhysicsUtility.ElasticCollision(thePlayer1.CurrentDirection * thePlayer1.CurrentSpeed,
                thePlayer2.CurrentDirection * thePlayer2.CurrentSpeed,
                thePlayer1.Weight, thePlayer2.Weight, thePlayer1.transform.position, thePlayer2.transform.position);
            
            (Vector3,Vector2) feedBackData1 = (info.Collision2D.contacts[0].point, 
                PhysicsUtility.SparkDir(thePlayer1.CurrentDirection, thePlayer2.CurrentDirection,thePlayer1.CurrentSpeed * 5));
            thePlayer1.VFX.Play("Hit",feedBackData1);
            (Vector3,Vector2) feedBackData2 = (info.Collision2D.contacts[0].point, 
                PhysicsUtility.SparkDir(thePlayer2.CurrentDirection, thePlayer1.CurrentDirection,thePlayer2.CurrentSpeed * 5));
            thePlayer2.VFX.Play("Hit",feedBackData2);
            
            thePlayer1.CurrentDirection = res.v1Prime.normalized;
            thePlayer2.CurrentDirection = res.v2Prime.normalized;
            thePlayer1.CurrentSpeed = res.v1Prime.magnitude;
            thePlayer2.CurrentSpeed = res.v2Prime.magnitude;
            

            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
        }
    }
}