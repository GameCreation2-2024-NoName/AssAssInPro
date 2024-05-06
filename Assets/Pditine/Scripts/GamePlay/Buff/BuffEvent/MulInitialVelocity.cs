using Pditine.Player;
using UnityEngine;

namespace Pditine.GamePlay.Buff
{
    [CreateAssetMenu(fileName = "MulInitialVelocity",menuName = "AssAssIn/BuffEvent/MulInitialVelocity")]
    public class MulInitialVelocity : BuffEvent
    {
        [SerializeField] private float initialVelocityMulAdjustment;
        public override void Trigger(BuffInfo buffInfo)
        {
            var thePlayer = buffInfo.target;
            thePlayer.initialVelocityMulAdjustment *= initialVelocityMulAdjustment;
        }
    }
}