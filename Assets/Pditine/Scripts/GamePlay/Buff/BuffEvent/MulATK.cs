using Pditine.Player;
using UnityEngine;

namespace Pditine.GamePlay.Buff
{
    [CreateAssetMenu(fileName = "MulATKEvent",menuName = "AssAssIn/BuffEvent/MulATK")]
    public class MulATK : BuffEvent
    {
        [SerializeField] private float atkMulAdjustment;
        public override void Trigger(BuffInfo buffInfo)
        {
            var thePlayer = buffInfo.target;
            thePlayer.atkMulAdjustment *= atkMulAdjustment;
            
        }
    }
}