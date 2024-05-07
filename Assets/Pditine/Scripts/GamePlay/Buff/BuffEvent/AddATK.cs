using UnityEngine;

namespace Pditine.GamePlay.Buff
{
    [CreateAssetMenu(fileName = "AddATK",menuName = "AssAssIn/BuffEvent/AddATK")]
    public class AddATK : BuffEvent
    {
        [SerializeField] private int atkAddAdjustment;
        public override void Trigger(BuffInfo buffInfo)
        {
            var thePlayer = buffInfo.target;
            thePlayer.atkAddAdjustment += atkAddAdjustment;
        }
    }
}