using Pditine.Audio;
using Pditine.Map;
using Pditine.Player.Thorn;
using UnityEngine;

namespace Pditine.Collide.CollideEvent
{
    public class Boundary_ThornEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "Boundary" && tag2 == "Thorn") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2)
        {
            var thePlayer = (collider2 as ThornBase).ThePlayer;
            var originDirection = thePlayer.Direction;
            Vector2 Out_Direction = Vector2.Reflect(originDirection,((Boundary)collider1).NormalDirection);
            thePlayer.Direction = Out_Direction;
            thePlayer.HitFeedback();
            //collider1.transform.GetComponent<VisualBox>()?.Act();
            //collider1.transform.position += (Vector3)thePlayer.Direction;
            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
        }
    }
}