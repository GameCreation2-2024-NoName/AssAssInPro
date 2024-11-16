using System;
using Pditine.GamePlay.Buff;
using Pditine.Player;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class UIManager : MonoBehaviour
    {
        //todo:UI框架
        // [SerializeField] protected PlayerCD cd1;
        // [SerializeField] protected PlayerCD cd2;
        [SerializeField] protected Timer timer;
        [SerializeField] protected Energy energySlider1;
        [SerializeField] protected Energy energySlider2;
        [SerializeField] protected HeadPicture head1;
        [SerializeField] protected HeadPicture head2;
        [SerializeField] protected PlayerHP hp1;
        [SerializeField] protected PlayerHP hp2;
        [SerializeField] protected BuffList buffList1;
        [SerializeField] protected BuffList buffList2;
        [SerializeField] protected SettlementPanel settlementPanel;
        [SerializeField] protected Image gaussianBlur;
        [SerializeField] protected GameObject pausePanel;
        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] protected Mark mark1;
        [SerializeField] protected Mark mark2;
        [SerializeField] protected ChargingBar chargingBar1;
        [SerializeField] protected ChargingBar chargingBar2;
        protected const float GaussianBlurAlpha = (float)83 / 255;
        public CanvasGroup CanvasGroup => canvasGroup;
        
        public static UIManager Instance { get; protected set; }

        protected void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Debug.LogWarning("单例重复");
                Destroy(gameObject);
            }
        }
        
        protected void OnEnable()
        {
            EventSystem.AddEventListener("Pause",SetPausePanelOpen);
            EventSystem.AddEventListener("UnPause",SetPausePanelUnOpen);
        }

        protected void OnDisable()
        {
            EventSystem.RemoveEventListener("Pause",SetPausePanelOpen);
            EventSystem.RemoveEventListener("UnPause",SetPausePanelUnOpen);
        }
        
        public void Init(PlayerController player1,PlayerController player2)
        {
            BindPlayer(player1);
            BindPlayer(player2);
            //todo:限时游戏
            timer.Init(true);

            BuffManager.Instance.OnAttachBuff += AddBuffUI;
            BuffManager.Instance.OnLostBuff += RemoveBuffUI;
        }

        protected void BindPlayer(PlayerController thePlayer)
        {
            if (thePlayer.ID == 1)
            {
                // cd1.Init(thePlayer);
                energySlider1.Init(thePlayer);
                head1.Init(thePlayer);
                hp1.Init(thePlayer);
                mark1.Init(thePlayer);
                chargingBar1.Init(thePlayer);
            }else if (thePlayer.ID == 2)
            {
                // cd2.Init(thePlayer);
                energySlider2.Init(thePlayer);
                head2.Init(thePlayer);
                hp2.Init(thePlayer);
                mark2.Init(thePlayer);
                chargingBar2.Init(thePlayer);
            }else PFCLog.Error("玩家ID错误");
        }
        
        protected void SetPausePanelOpen()
        {
            pausePanel.SetActive(true);
            var color = gaussianBlur.color;
            gaussianBlur.color = new Color(color.r, color.g, color.b, GaussianBlurAlpha);
        }
        
        protected void SetPausePanelUnOpen()
        {
            pausePanel.SetActive(false);
            var color = gaussianBlur.color;
            gaussianBlur.color = new Color(color.r, color.g, color.b, 0);
        }
        
        
        public void GameOver(PlayerController theWinner)
        {
            DelayUtility.Delay(2.5f, () =>
            {
                FadeUtility.FadeInAndStay(gaussianBlur, 40, null, GaussianBlurAlpha);
                settlementPanel.Init(theWinner);
            });
        }
        
        protected void AddBuffUI(BuffInfo buffInfo)
        {
            if (buffInfo.durationCounter <= 0) return;
            if (!buffInfo.buffData.showInUI) return;
            if(buffInfo.target.ID == 1)
                buffList1.AddBuff(buffInfo);
            else if(buffInfo.target.ID == 2)
                buffList2.AddBuff(buffInfo);
            else 
                Debug.LogError("玩家ID错误");
        }
        
        protected void RemoveBuffUI(BuffInfo buffInfo)
        {
            if (!buffInfo.buffData.showInUI) return;
            if(buffInfo.target.ID == 1)
                buffList1.RemoveBuff(buffInfo);
            else if(buffInfo.target.ID == 2)
                buffList2.RemoveBuff(buffInfo);
            else 
                Debug.LogError("玩家ID错误");
        }

        public void ShowUI()
        {
            FadeUtility.FadeInAndStay(canvasGroup,80);
        }
        
        public void HideUI()
        {
            canvasGroup.alpha = 0;
        }
    }
}