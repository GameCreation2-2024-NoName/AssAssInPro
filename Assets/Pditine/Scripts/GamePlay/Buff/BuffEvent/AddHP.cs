using UnityEngine;

namespace Pditine.GamePlay.Buff
{
    [CreateAssetMenu(fileName = "AddHP",menuName = "AssAssIn/BuffEvent/AddHP")]
    public class AddHP : BuffEvent
    {
        [SerializeField] private int hpAddAdjustment;
        public override void Trigger(BuffInfo buffInfo)
        {
            var thePlayer = buffInfo.target;
            thePlayer.ChangeHP(hpAddAdjustment);
        }
    }
}