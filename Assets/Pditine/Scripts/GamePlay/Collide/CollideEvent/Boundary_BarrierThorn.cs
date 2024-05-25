﻿using Hmxs.Scripts;
using Pditine.Audio;
using Pditine.Map;
using UnityEngine;

namespace Pditine.Collide.CollideEvent
{
    public class Boundary_BarrierThorn : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "Boundary" && tag2 == "BarrierThorn") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2,CollideInfo info)
        {
            var theBarrier= (collider2 as BarrierThorn).TheBarrier;
            var originDirection = theBarrier.Direction;
            Vector2 Out_Direction = Vector2.Reflect(originDirection,((Boundary)collider1).NormalDirection);
            theBarrier.Direction = Out_Direction;
            //collider1.transform.GetComponent<VisualBox>()?.Act();
            theBarrier.collideWithBoundary.PlayFeedbacks();
            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
        }
    }
}