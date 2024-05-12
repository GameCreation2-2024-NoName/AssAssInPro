using Pditine.Player;

namespace Hmxs.Scripts.SinglePlayer
{
    public class NormalAIPlayer : PlayerController
    {


        protected override void OnStart()
        {
        }

        protected override void OnUpdate()
        {
            if (!canMove) return;
            if (IsPause) return;
        }
    }
}