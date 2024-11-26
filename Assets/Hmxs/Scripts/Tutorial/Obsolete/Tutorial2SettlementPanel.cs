using System;
using Pditine.Data;
using Pditine.GamePlay.UI;
using Pditine.Player;
using PurpleFlowerCore;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Scripts.Tutorial
{
    [Obsolete]
    public class Tutorial2SettlementPanel : SettlementPanel
    {
        [SerializeField] protected Text titleText;
        [SerializeField] protected GameObject nextLevel;
        [SerializeField] protected GameObject tryAgain;

        public override void Init(PlayerController thePlayer)
        {
            base.Init(thePlayer);

            titleText.text = thePlayer.ID == 2 ?
                "你的ASS被对方戳爆了…" :
                "你戳爆了对方的ASS!!!";
            if (thePlayer.ID == 1)
            {
                nextLevel.SetActive(true);
                tryAgain.SetActive(false);
            }
            else
            {
                nextLevel.SetActive(false);
                tryAgain.SetActive(true);
            }
        }

        public void TryAgain()
        {
            DataManager.Instance.PassingData.mainMenuOpenedMenuIndex = 0;
            SceneSystem.LoadScene(7);
        }

        public void NextLevel()
        {
            DataManager.Instance.PassingData.mainMenuOpenedMenuIndex = 7;
            SceneSystem.LoadScene(0);
        }
    }
}