using LJH.Scripts.Player;
using UnityEngine;

namespace LJH.LightBall
{
    public class BlueLightBall : LightBallBase
    {
        protected override void AddBuff(PlayerController thePlayer)
        {
            thePlayer.TheAss.ChangeScale(-0.2f);
        }
    }
}