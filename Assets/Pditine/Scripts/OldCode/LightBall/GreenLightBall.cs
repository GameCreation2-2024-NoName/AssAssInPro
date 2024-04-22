using LJH.Scripts.Player;
using Pditine.Scripts.Player;

namespace LJH.LightBall
{
    public class GreenLightBall : LightBallBase
    {
        protected override void AddBuff(PlayerController thePlayer)
        {
            thePlayer.ChangeSpeed(20);
        }
    }
}