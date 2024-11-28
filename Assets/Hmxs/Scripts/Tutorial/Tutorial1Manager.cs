using Pditine.Data;
using Pditine.GamePlay.Buff;
using Pditine.GamePlay.GameManager;
using Pditine.GamePlay.UI;
using Pditine.Player;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
using PurpleFlowerCore.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs.Scripts.Tutorial
{
    public class Tutorial1Manager : GameManagerBase<Tutorial1Manager>
    {
        public PlayerController Player => player1;
        public Transform PlayerTransform => player1.transform;

        [Required] [SerializeField] private GameObject player1Thorn;
        [Required] [SerializeField] private GameObject player1Ass;
        [Required] [SerializeField] private GameObject player2Thorn;
        [Required] [SerializeField] private GameObject player2Ass;

        protected override void Update() { } // Disable Pause

        protected override void Init()
        {
            DataManager.Instance.PassingData.player1AssID = player1Ass.GetComponent<AssBase>().Data.ID;
            DataManager.Instance.PassingData.player1ThornID = player1Thorn.GetComponent<ThornBase>().Data.ID;
            DataManager.Instance.PassingData.player2AssID = player2Ass.GetComponent<AssBase>().Data.ID;
            DataManager.Instance.PassingData.player2ThornID = player2Thorn.GetComponent<ThornBase>().Data.ID;

            var thorn1 = player1Thorn.GetComponent<ThornBase>();
            var ass1 = player1Ass.GetComponent<AssBase>();
            var thorn2 = player2Thorn.GetComponent<ThornBase>();
            var ass2 = player2Ass.GetComponent<AssBase>();
            if (!thorn1 || !thorn2 || !ass1 || !ass2)
            {
                Debug.LogError("cont get ass/thorn base");
                return;
            }
            player1.Init(thorn1, ass1);
            player2.Init(thorn2, ass2);
            thorn1.Init(player1);
            ass1.Init(player1);
            thorn2.Init(player2);
            ass2.Init(player2);

            BuffManager.Instance.Init(player1,player2);
            UIManager.Instance.Init(player1,player2);

            startEffect.PlayFeedbacks();
            DelayUtility.Delay(4.7f,()=>
            {
                PlayerCanMove(true);
                PlayerManager.Instance.SwitchMap("GamePlay");
            });
        }
    }
}