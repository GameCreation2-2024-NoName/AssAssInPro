﻿using Pditine.Audio;
using Pditine.Map;
using Pditine.Player.Thorn;
using UnityEngine;

namespace Pditine.Collide.CollideEvent
{
    public class DestructibleBox_ThornEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "DestructibleBox" && tag2 == "Thorn") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2,CollideInfo info)
        {
            var thePlayer = (collider2 as ThornBase).ThePlayer;
            var originDirection = thePlayer.CurrentDirection;
            Vector2 Out_Direction = Vector2.Reflect(originDirection,info.Collision2D.contacts[0].normal);
            thePlayer.CurrentDirection = Out_Direction;
            thePlayer.HitFeedback();
            
            (collider1 as DestructibleBox).BeAttack();
            
            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
        }
    }
}