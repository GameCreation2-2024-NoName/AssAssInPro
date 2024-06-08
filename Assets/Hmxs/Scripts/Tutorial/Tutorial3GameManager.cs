using Hmxs.Toolkit.Plugins.Fungus.FungusTools;
using Pditine.Data;
using Pditine.GamePlay.Buff;
using Pditine.GamePlay.GameManager;
using Pditine.GamePlay.UI;
using Pditine.Player;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs.Scripts.Tutorial
{
    public class Tutorial3GameManager : GameManagerBase<Tutorial3GameManager>
    {
        public Transform Player1 => player1.transform;
        public PlayerController Player1Controller => player1;

        [Required] [SerializeField] private GameObject player2Thorn;
        [Required] [SerializeField] private GameObject player2Ass;
        [Required] [SerializeField] private SpriteRenderer barrier1ThornSprite;
        [Required] [SerializeField] private SpriteRenderer barrier1AssSprite;
        [Required] [SerializeField] private SpriteRenderer barrier2AssSprite;
        [Required] [SerializeField] private SpriteRenderer barrier2ThornSprite;

        protected override void Update()
        {
            // Disable Pause
        }

        protected override void Init()
        {
            CreatePlayer(PassingData.player1AssID, PassingData.player1ThornID, player1);

            DataManager.Instance.PassingData.player2AssID = player2Ass.GetComponent<AssBase>().Data.ID;
            DataManager.Instance.PassingData.player2ThornID = player2Thorn.GetComponent<ThornBase>().Data.ID;

            var thorn2 = player2Thorn.GetComponent<ThornBase>();
            var ass2 = player2Ass.GetComponent<AssBase>();
            if (!thorn2 || ! ass2)
            {
                Debug.LogError("cont get ass/thorn base");
                return;
            }
            thorn2.Init(player2);
            ass2.Init(player2);
            player2.Init(thorn2, ass2);

            BuffManager.Instance.Init(player1, player2);
            UIManager.Instance.Init(player1, player2);

            FlowchartManager.ExecuteBlock("Tutorial1Start");
        }

        public void EnablePlayerInput() => PlayerManager.Instance.SwitchMap("GamePlay");

        public void SetPlayer1Movable(bool movable) => player1.canMove = movable;
        public void SetPlayer2Movable(bool movable) => player2.canMove = movable;

        public void PlayStartFeedback() => startEffect.PlayFeedbacks();

        public void SetHighlight(SpriteRenderer sprite) => sprite.sortingOrder = 99;

        public void ExecuteBlock(string blockName) => FlowchartManager.ExecuteBlock(blockName);

        public void ResetHighlight()
        {
            barrier1AssSprite.sortingOrder = 1;
            barrier1ThornSprite.sortingOrder = 1;
            barrier2AssSprite.sortingOrder = 1;
            barrier2ThornSprite.sortingOrder = 1;
        }
    }
}