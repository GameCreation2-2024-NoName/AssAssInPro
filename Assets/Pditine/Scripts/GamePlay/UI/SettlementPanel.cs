using System;
using Pditine.Data;
using Pditine.Player;
using PurpleFlowerCore;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class SettlementPanel : MonoBehaviour
    {
        [SerializeField] private Image backgroundLight;
        //[SerializeField] private Image sunglasses;
        [SerializeField] private Image thorn;
        [SerializeField] private Image ass;
        [SerializeField] private Animator theAnimator;
        [SerializeField]private float speed;
        private bool _showing;
        
        private void FixedUpdate()
        {
            if(_showing)
                backgroundLight.transform.Rotate(0,0,speed);
        }

        public void Init(PlayerController thePlayer)
        {
            theAnimator.SetTrigger("Init");
            _showing = true;
            thorn.sprite = thePlayer.TheThorn.Data.Portrait;
            ass.sprite = thePlayer.TheAss.Data.Portrait;
            thorn.SetNativeSize();
            ass.SetNativeSize();
        }

        public void BackToMainMenu()
        {
            DataManager.Instance.PassingData.mainMenuOpenedMenuIndex = 0;
            SceneSystem.LoadScene(0);
        }

        public void AnotherGame()
        {
            DataManager.Instance.PassingData.mainMenuOpenedMenuIndex = 4;
            SceneSystem.LoadScene(0);
        }
    }
}