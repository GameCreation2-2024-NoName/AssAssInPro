using LJH.Scripts.Player;
using UnityEngine;

namespace LJH.LightBall
{
    public class YellowLightBall : LightBallBase
    {
        protected override void AddBuff(PlayerController thePlayer)
        {
            thePlayer.ChangeSpeed(-15);
        }
    }
}