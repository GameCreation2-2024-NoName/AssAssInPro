using Pditine.Audio;
using Pditine.GamePlay.Camera;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
using Pditine.Utility;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.Collide.CollideEvent
{
    public class Thorn_AssEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "Thorn" && tag2 == "Ass") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2)
        {
            var thePlayer1 = (collider1 as ThornBase).ThePlayer;
            var thePlayer2 = (collider2 as AssBase).ThePlayer;

            //thePlayer1.HitFeedback();
            thePlayer2.BeHitAssFeedback();
            thePlayer2.ChangeHP(-thePlayer1.ATK);
            
            var res =
                PhysicsUtility.ElasticCollision(thePlayer1.Direction * thePlayer1.CurrentSpeed,
                    thePlayer2.Direction * thePlayer2.CurrentSpeed,
                    thePlayer1.Weight, thePlayer2.Weight, thePlayer1.transform.position, thePlayer2.transform.position);
            thePlayer1.Direction = res.v1Prime.normalized;
            thePlayer2.Direction = res.v2Prime.normalized;
            thePlayer1.CurrentSpeed = res.v1Prime.magnitude;
            thePlayer2.CurrentSpeed = res.v2Prime.magnitude;
            
            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
            CameraManager.Instance.OnCollidePLayerAss(thePlayer2.ID);
            
            (collider1 as ThornBase).OnAttack?.Invoke();
            (collider2 as AssBase).OnBeAttackByThorn?.Invoke();
            (collider2 as AssBase).OnBeAttack?.Invoke();
        }
    }
}