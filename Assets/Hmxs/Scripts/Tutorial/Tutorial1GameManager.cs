using Pditine.GamePlay.Buff;
using Pditine.GamePlay.GameManager;
using Pditine.GamePlay.UI;
using PurpleFlowerCore.Utility;

namespace Hmxs.Scripts.Tutorial
{
    public class Tutorial1GameManager : GameManagerBase<Tutorial1GameManager>
    {
        protected override void Init()
        {
            PassingData.player1AssID = 0;
            PassingData.player2ThornID = 0;

            CreatePlayer(PassingData.player1AssID, PassingData.player1ThornID, player1);
            CreatePlayer(PassingData.player2AssID, PassingData.player2ThornID, player2);

            // BuffManager.Instance.Init(player1,player2);
            // UIManager.Instance.Init(player1,player2);

            // startEffect.PlayFeedbacks();
            // DelayUtility.Delay(4.7f,()=>
            // {
            //     PlayerCanMove(true);
            //     PlayerManager.Instance.SwitchMap("GamePlay");
            // });
        }
    }
}