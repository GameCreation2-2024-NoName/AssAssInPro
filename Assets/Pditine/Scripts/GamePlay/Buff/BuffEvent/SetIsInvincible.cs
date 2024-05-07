using System;
using UnityEngine;

namespace Pditine.GamePlay.Buff
{
    [CreateAssetMenu(fileName = "SetIsInvincible",menuName = "AssAssIn/BuffEvent/SetIsInvincible")]
    public class SetIsInvincible : BuffEvent
    {
        [SerializeField] private bool isInvincible;
        public override void Trigger(BuffInfo buffInfo)
        {
            buffInfo.target.isInvincible = isInvincible;
        }
    }
}