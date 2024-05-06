using Pditine.Data;
using Pditine.GamePlay.Buff;
using Pditine.Player;
using UnityEngine;

namespace Pditine.GamePlay.LightBall
{
    public class AddHP : LightBallBase
    {
        protected override BuffInfo AddBuff(PlayerController targetPlayer)
        {
            return new BuffInfo(DataManager.Instance.GetBuffData("AddHP"),gameObject,targetPlayer);
        }
    }
}