using PurpleFlowerCore;
using UnityEngine;
using PurpleFlowerCore.Utility;

namespace Pditine.GamePlay.Buff
{
    [Configurable("Buff/BuffEvent")]
    [CreateAssetMenu(fileName = "RemoveBuffStackWhenBeAttack",menuName = "AssAssIn/BuffEvent/RemoveBuffStackWhenBeAttack")]
    public class RemoveBuffStackWhenBeAttack : BuffEvent
    {
        public override void Trigger(BuffInfo buffInfo)
        {
            var thePlayer = buffInfo.target;
            thePlayer.OnTryChangeHP += _ =>
            {
                BuffManager.Instance.LostBuff(buffInfo);
                //Debug.Log("lost shield buff");
            };
        }
    }
}