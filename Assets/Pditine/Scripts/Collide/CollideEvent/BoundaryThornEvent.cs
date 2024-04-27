using LJH.Scripts.Map;
using Pditine.Player.Thorn;
using UnityEngine;

namespace Pditine.Collide.CollideEvent
{
    public class BoundaryThornEvent : CollidingEventBase
    {
        public override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "Boundary" && tag2 == "Thorn") return true;
            if (tag1 == "Thorn" && tag2 == "Boundary") return true;
            return false;
        }

        public override void Happen(ColliderBase collider1, ColliderBase collider2)
        {
            var thePlayer = ((ThornBase)collider2).ThePlayer;
            var originDirection = thePlayer.Direction;
            Vector2 outDirection = Vector2.Reflect(originDirection,((Boundary)collider1).NormalDirection);
            thePlayer.Direction = outDirection;
            thePlayer.HitFeedback();
            //collider1.transform.GetComponent<VisualBox>()?.Act();
        }
    }
}