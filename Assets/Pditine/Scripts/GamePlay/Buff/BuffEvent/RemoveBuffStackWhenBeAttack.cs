using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.GamePlay.Buff
{
    [CreateAssetMenu(fileName = "RemoveBuffStackWhenBeAttack",menuName = "AssAssIn/BuffEvent/RemoveBuffStackWhenBeAttack")]
    public class RemoveBuffStackWhenBeAttack : BuffEvent
    {
        public override void Trigger(BuffInfo buffInfo)
        {
            var thePlayer = buffInfo.target;
            thePlayer.OnTryChangeHP += (hp) =>
            {
                BuffManager.Instance.LostBuff(buffInfo);
                //Debug.Log("lost shield buff");
            };
        }
    }
}