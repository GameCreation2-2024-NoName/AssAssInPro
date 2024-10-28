using Pditine.Audio;
using Pditine.Data;
using Pditine.GamePlay.Buff;
using Pditine.Map;
using Pditine.Player;
using Pditine.Player.Ass;
using Pditine.Utility;
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

        protected override void Happen(ColliderBase collider1, ColliderBase collider2,CollideInfo info)
        {
            var thePlayer = (collider2 as AssBase).ThePlayer;
            var theBarrier = (collider1 as BarrierThorn).TheBarrier;

            theBarrier.HitFeedback.PlayFeedbacks();
            thePlayer.VFX[VFXName.AssHit].Play();
            thePlayer.ChangeHP(-theBarrier.ATK);
            
            BuffManager.Instance.AttachBuff(new BuffInfo(DataManager.Instance.GetBuffData(9),null,thePlayer));
            
            var res =
                PhysicsUtility.ElasticCollision(thePlayer.CurrentDirection * thePlayer.CurrentSpeed,
                    theBarrier.Direction * theBarrier.CurrentSpeed,
                    thePlayer.Weight, theBarrier.Weight, thePlayer.transform.position, theBarrier.transform.position);
            thePlayer.CurrentDirection = res.v1Prime.normalized;
            theBarrier.Direction = res.v2Prime.normalized;
            thePlayer.CurrentSpeed = res.v1Prime.magnitude;
            theBarrier.CurrentSpeed = res.v2Prime.magnitude;
            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
            
            
            
            (collider2 as AssBase).OnBeAttack?.Invoke(collider1);
        }
    }
}