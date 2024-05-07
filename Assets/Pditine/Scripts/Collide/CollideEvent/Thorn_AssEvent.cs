using Pditine.Audio;
using Pditine.ClassicGame;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
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

            thePlayer1.HitFeedback();
            thePlayer2.ChangeHP(-thePlayer1.ATK);

            float deltaSpeed = thePlayer1.CurrentSpeed * Mathf.Abs(
                               Mathf.Cos(Vector2.Angle(thePlayer1.Direction, thePlayer2.Direction)));
            thePlayer2.CurrentSpeed += deltaSpeed;
            thePlayer1.Direction = Vector2.Reflect(thePlayer1.Direction, -thePlayer2.Direction);
            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
            
            (collider1 as ThornBase).OnAttack?.Invoke();
            (collider2 as AssBase).OnBeAttack?.Invoke();
        }
    }
}