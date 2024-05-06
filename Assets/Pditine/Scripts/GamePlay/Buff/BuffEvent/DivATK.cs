using Pditine.Player;
using UnityEngine;

namespace Pditine.GamePlay.Buff
{
    [CreateAssetMenu(fileName = "DivATK",menuName = "AssAssIn/BuffEvent/DivATK")]
    public class DivATK : BuffEvent
    {
        [SerializeField] private float atkDivAdjustment;
        public override void Trigger(BuffInfo buffInfo)
        {
            var thePlayer = buffInfo.target;
            thePlayer.atkMulAdjustment /= atkDivAdjustment;
        }
    }
}