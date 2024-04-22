using LJH.Scripts.Player;
using Pditine.Scripts.Player;

namespace LJH.LightBall
{
    public class RedLightBall : LightBallBase
    {
        protected override void AddBuff(PlayerController thePlayer)
        {
            thePlayer.TheThorn.ChangeScale(0.3f);
        }
    }
}