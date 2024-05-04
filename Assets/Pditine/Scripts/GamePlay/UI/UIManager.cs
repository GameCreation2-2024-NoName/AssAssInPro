using Pditine.Player;
using PurpleFlowerCore;
using UnityEngine;

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
        [SerializeField] private SettlementPanel settlementPanel;
        
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
        
        public void Init(PlayerController thePlayer)
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

        public void GameOver(PlayerController theWinner)
        {
            settlementPanel.Init(theWinner);
        }
    }
}