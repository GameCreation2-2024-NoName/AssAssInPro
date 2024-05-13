using UnityEngine;

namespace Pditine.GamePlay.Buff
{
    [CreateAssetMenu(fileName = "SetCanMove",menuName = "AssAssIn/BuffEvent/SetCanMove")]
    public class SetCanMove : BuffEvent
    {
        [SerializeField] private bool canMove;
        public override void Trigger(BuffInfo buffInfo)
        {
            buffInfo.target.canMove = canMove;
        }
    }
}