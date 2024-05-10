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
        [SerializeField] private PlayerCD cd1;
        [SerializeField] private PlayerCD cd2;
        [SerializeField] private HeadPicture head1;
        [SerializeField] private HeadPicture head2;
        [SerializeField] private PlayerHP hp1;
        [SerializeField] private PlayerHP hp2;
        [SerializeField] private BuffList buffList1;
        [SerializeField] private BuffList buffList2;
        [SerializeField] private SettlementPanel settlementPanel;
        [SerializeField] private Image gaussianBlur;
        [SerializeField] private GameObject pausePanel;

        private const float GaussianBlurAlpha = (float)83 / 255; 
        public static UIManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Debug.LogWarning("单例重复");
                Destroy(gameObject);
            }
        }
        
        private void OnEnable()
        {
            EventSystem.AddEventListener("Pause",SetPausePanelOpen);
            EventSystem.AddEventListener("UnPause",SetPausePanelUnOpen);
        }

        private void OnDisable()
        {
            EventSystem.RemoveEventListener("Pause",SetPausePanelOpen);
            EventSystem.RemoveEventListener("UnPause",SetPausePanelUnOpen);
        }
        
        public void Init(PlayerController player1,PlayerController player2)
        {
            BindPlayer(player1);
            BindPlayer(player2);

            BuffManager.Instance.OnAttachBuff += AddBuffUI;
            BuffManager.Instance.OnLostBuff += RemoveBuffUI;
        }

        private void BindPlayer(PlayerController thePlayer)
        {
            if (thePlayer.ID == 1)
            {
                cd1.Init(thePlayer);
                head1.Init(thePlayer);
                hp1.Init(thePlayer);
            }else if (thePlayer.ID == 2)
            {
                cd2.Init(thePlayer);
                head2.Init(thePlayer);
                hp2.Init(thePlayer);
            }else PFCLog.Error("玩家ID错误");
        }
        
        private void SetPausePanelOpen()
        {
            pausePanel.SetActive(true);
            var color = gaussianBlur.color;
            gaussianBlur.color = new Color(color.r, color.g, color.b, GaussianBlurAlpha);
        }
        
        private void SetPausePanelUnOpen()
        {
            pausePanel.SetActive(false);
            var color = gaussianBlur.color;
            gaussianBlur.color = new Color(color.r, color.g, color.b, 0);
        }
        
        
        public void GameOver(PlayerController theWinner)
        {
            DelayUtility.Delay(3.2f, () =>
            {
                FadeUtility.FadeInAndStay(gaussianBlur, 40, null, GaussianBlurAlpha);
                settlementPanel.Init(theWinner);
            });
        }

        private void AddBuffUI(BuffInfo buffInfo)
        {
            if (buffInfo.durationCounter <= 0) return;
            if(buffInfo.target.ID == 1)
                buffList1.AddBuff(buffInfo);
            else if(buffInfo.target.ID == 2)
                buffList2.AddBuff(buffInfo);
            else 
                Debug.LogError("玩家ID错误");
        }
        
        private void RemoveBuffUI(BuffInfo buffInfo)
        {
            if(buffInfo.target.ID == 1)
                buffList1.RemoveBuff(buffInfo);
            else if(buffInfo.target.ID == 2)
                buffList2.RemoveBuff(buffInfo);
            else 
                Debug.LogError("玩家ID错误");
        }
    }
}