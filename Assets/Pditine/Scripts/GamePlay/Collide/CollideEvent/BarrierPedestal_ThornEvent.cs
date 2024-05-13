using Pditine.Audio;
using Pditine.Map;
using Pditine.Player.Thorn;
using PurpleFlowerCore;

namespace Pditine.Collide.CollideEvent
{
    public class BarrierPedestal_ThornEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "BarrierPedestal" && tag2 == "Thorn") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2)
        {
                var thePlayer = (collider2 as ThornBase).ThePlayer;
                var theBarrier = (collider1 as BarrierPedestal).TheBarrier; 
                
                (collider1 as BarrierPedestal).TheBarrier.TheThorn.AddCollider(collider2);
                collider2.AddCollider((collider1 as BarrierPedestal).TheBarrier.TheThorn);
                
                theBarrier.Direction = thePlayer.Direction;
                thePlayer.Direction = -thePlayer.Direction;
                theBarrier.CurrentSpeed = thePlayer.CurrentSpeed/1.5f;
                thePlayer.CurrentSpeed /= 1.2f;
                thePlayer.HitFeedback();
                AAIAudioManager.Instance.PlayEffect("碰撞音效1");
        }
    }
}