﻿using Pditine.Audio;
using Pditine.Map;
using Pditine.Player.Thorn;
using Pditine.Utility;
using UnityEngine;

namespace Pditine.Collide.CollideEvent
{
    public class BarrierThorn_ThornEvent: CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "BarrierThorn" && tag2 == "Thorn") return true;
            return false;
        }

        protected override void Happen(ColliderBase collider1, ColliderBase collider2,CollideInfo info)
        {
            var thePlayer = (collider2 as ThornBase).ThePlayer;
            var theBarrier = (collider1 as BarrierThorn).TheBarrier;
            
            (collider1 as BarrierThorn).TheBarrier.ThePedestal.AddCollider(collider2);
            collider2.AddCollider((collider1 as BarrierThorn).TheBarrier.ThePedestal);
            
            var res =
                PhysicsUtility.ElasticCollision(thePlayer.CurrentDirection * thePlayer.CurrentSpeed,
                    theBarrier.Direction * theBarrier.CurrentSpeed,
                    thePlayer.Weight, theBarrier.Weight, thePlayer.transform.position, theBarrier.transform.position);
            
            (Vector3,Vector2) feedBackData = (info.Collision2D.contacts[0].point, 
                PhysicsUtility.SparkDir(thePlayer.CurrentDirection,theBarrier.Direction,thePlayer.CurrentSpeed * 5));
            thePlayer.VFX.Play("Hit",feedBackData);
            
            thePlayer.CurrentDirection = res.v1Prime.normalized;
            theBarrier.Direction = res.v2Prime.normalized;
            thePlayer.CurrentSpeed = res.v1Prime.magnitude;
            theBarrier.CurrentSpeed = res.v2Prime.magnitude;
            
            
            theBarrier.HitFeedback.PlayFeedbacks();
            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
        }
    }
}