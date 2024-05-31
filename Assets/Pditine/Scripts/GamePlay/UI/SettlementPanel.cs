using System;
using Pditine.Data;
using Pditine.Player;
using PurpleFlowerCore;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class SettlementPanel : MonoBehaviour
    {
        [SerializeField] protected Image backgroundLight;
        [SerializeField] protected Image thorn;
        [SerializeField] protected Image ass;
        [SerializeField] protected Animator theAnimator;
        [SerializeField] protected float speed;
        protected bool Showing;
        
        private void FixedUpdate()
        {
            if(Showing)
                backgroundLight.transform.Rotate(0,0,speed);
        }

        public virtual void Init(PlayerController thePlayer)
        {
            theAnimator.SetTrigger("Init");
            Showing = true;
            thorn.sprite = thePlayer.TheThorn.Data.Portrait;
            
            ass.sprite = thePlayer.ID == 1? thePlayer.TheAss.Data.PortraitBlue: thePlayer.TheAss.Data.PortraitYellow;
            thorn.SetNativeSize();
            ass.SetNativeSize();
        }
        
        public void AnotherGame()
        {
            DataManager.Instance.PassingData.mainMenuOpenedMenuIndex = 3;
            SceneSystem.LoadScene(0);
        }
        
        public void BackToMainMenu()
        {
            DataManager.Instance.PassingData.mainMenuOpenedMenuIndex = 0;
            SceneSystem.LoadScene(0);
        }
    }
}