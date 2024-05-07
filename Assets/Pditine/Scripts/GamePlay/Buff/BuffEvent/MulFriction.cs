using UnityEngine;

namespace Pditine.GamePlay.Buff
{
    [CreateAssetMenu(fileName = "MulFriction",menuName = "AssAssIn/BuffEvent/MulFriction")]
    public class MulFriction : BuffEvent
    {
        [SerializeField] private float frictionMulAdjustment;
        public override void Trigger(BuffInfo buffInfo)
        {
            var thePlayer = buffInfo.target;
            thePlayer.frictionMulAdjustment *= frictionMulAdjustment;
        }
    }
}