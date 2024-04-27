using Pditine.Player.Thorn;

namespace Pditine.Collide.CollideEvent
{
    public class Thorn_ThornEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "Thorn" && tag2 == "Thorn") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2)
        {
            var thePlayer1 = (collider1 as ThornBase).ThePlayer;
            var thePlayer2 = (collider2 as ThornBase).ThePlayer;
            (thePlayer1.Direction, thePlayer2.Direction) = (thePlayer2.Direction, thePlayer1.Direction);
            (thePlayer1.CurrentSpeed, thePlayer2.CurrentSpeed) = (thePlayer2.CurrentSpeed, thePlayer1.CurrentSpeed);
            thePlayer1.HitFeedback();
            thePlayer2.HitFeedback();
        }
    }
}