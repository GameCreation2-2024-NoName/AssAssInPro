using Pditine.Audio;
using Pditine.ClassicGame;
using Pditine.Map;
using Pditine.Player.Ass;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.Collide.CollideEvent
{
    public class BarrierThorn_AssEvent: CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "BarrierThorn" && tag2 == "Ass") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2)
        {
            //CameraMoveUtility.MoveAndZoom(collider2.transform.position,0.03f,4);
            var thePlayer = (collider2 as AssBase).ThePlayer;
            var theBarrier = (collider1 as BarrierThorn).TheBarrier;
            //ClassicGameManager.Instance.PlayerDead(thePlayer.transform,thePlayer.ID);
            //thePlayer.BeDestroy();
            //thePlayer.LoseFeedback();
            theBarrier.HitFeedback.PlayFeedbacks();
            thePlayer.ChangeHP(-1);
            //EventSystem.EventTrigger("GameOver");
            
            float deltaSpeed = theBarrier.CurrentSpeed *
                               Mathf.Cos(Vector2.Angle(thePlayer.Direction, theBarrier.Direction));
            thePlayer.CurrentSpeed += deltaSpeed;
            //theBarrier.CurrentSpeed -= deltaSpeed;
            theBarrier.Direction = Vector2.Reflect(theBarrier.Direction, -theBarrier.Direction);
            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
        }
    }
}