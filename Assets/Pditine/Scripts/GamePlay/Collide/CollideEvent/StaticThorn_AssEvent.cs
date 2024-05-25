using Pditine.Audio;
using Pditine.Data;
using Pditine.GamePlay.Buff;
using Pditine.GamePlay.Camera;
using Pditine.Map;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
using Pditine.Utility;
using UnityEngine;

namespace Pditine.Collide.CollideEvent
{
    public class StaticThorn_AssEvent : CollidingEventBase
    {
        protected override bool CompareTag(string tag1, string tag2)
        {
            if (tag1 == "StaticThorn" && tag2 == "Ass") return true;
            return false;
        }
        protected override void Happen(ColliderBase collider1, ColliderBase collider2, CollideInfo info)
        {
            var theThorn = collider1 as StaticThorn;
            var thePlayer = (collider2 as AssBase).ThePlayer;

            thePlayer.CurrentDirection = theThorn.transform.right;
            thePlayer.CurrentSpeed += theThorn.PushForce;
            
            
            AAIAudioManager.Instance.PlayEffect("碰撞音效1");
            
            thePlayer.BeHitAssFeedback();
            thePlayer.ChangeHP(-theThorn.ATK);
            
            BuffManager.Instance.AttachBuff(new BuffInfo(DataManager.Instance.GetBuffData(9),null,thePlayer));
            
            (collider2 as AssBase).OnBeAttackByThorn?.Invoke(collider1);
            (collider2 as AssBase).OnBeAttack?.Invoke(collider1);
        }
    }
}