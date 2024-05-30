using Hmxs.Toolkit.Plugins.Fungus.FungusTools;
using Pditine.GamePlay.Buff;
using Pditine.GamePlay.GameManager;
using Pditine.GamePlay.UI;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs.Scripts.Tutorial
{
    public class Tutorial1GameManager : GameManagerBase<Tutorial1GameManager>
    {
        [Required] [SerializeField] private GameObject player1Thorn;
        [Required] [SerializeField] private GameObject player1Ass;
        [Required] [SerializeField] private GameObject player2Thorn;
        [Required] [SerializeField] private GameObject player2Ass;
        private SpriteRenderer _player1ThornSprite;
        private SpriteRenderer _player1AssSprite;
        private SpriteRenderer _player2ThornSprite;
        private SpriteRenderer _player2AssSprite;

        protected override void Init()
        {
            _player1AssSprite = player1Ass.GetComponent<SpriteRenderer>();
            _player1ThornSprite = player1Thorn.GetComponent<SpriteRenderer>();
            _player2AssSprite = player2Ass.GetComponent<SpriteRenderer>();
            _player2ThornSprite = player2Thorn.GetComponent<SpriteRenderer>();

            var thorn1 = player1Thorn.GetComponent<ThornBase>();
            var ass1 = player1Ass.GetComponent<AssBase>();
            var thorn2 = player2Thorn.GetComponent<ThornBase>();
            var ass2 = player2Ass.GetComponent<AssBase>();
            if (!thorn1 || !thorn2 || !ass1 || ! ass2)
            {
                Debug.LogError("cont get ass/thorn base");
                return;
            }
            thorn1.Init(player1);
            ass1.Init(player1);
            thorn2.Init(player2);
            ass2.Init(player2);
            player1.Init(thorn1, ass1);
            player2.Init(thorn2, ass2);

            BuffManager.Instance.Init(player1,player2);
            UIManager.Instance.Init(player1,player2);

            FlowchartManager.ExecuteBlock("Tutorial1Start");
        }

        public void EnablePlayerInput()
        {
            PlayerManager.Instance.SwitchMap("GamePlay");
        }

        public void SetPlayer1Movable(bool movable) => player1.canMove = movable;
        public void SetPlayer2Movable(bool movable) => player2.canMove = movable;

        public void SetDashInput(bool state)
        {
            if (state)
                PlayerManager.Instance.Handler1.DashAction.Enable();
            else
                PlayerManager.Instance.Handler1.DashAction.Disable();
        }

        public void SetDirectionInput(bool state)
        {
            if (state)
                PlayerManager.Instance.Handler1.DirectionAction.Enable();
            else
                PlayerManager.Instance.Handler1.DirectionAction.Disable();
        }

        public void PlayStartFeedback() => startEffect.PlayFeedbacks();

        public void SetHighlight(SpriteRenderer sprite) => sprite.sortingOrder = 99;

        public void ExecuteBlock(string blockName) => FlowchartManager.ExecuteBlock(blockName);

        public void ResetHighlight()
        {
            _player1AssSprite.sortingOrder = 1;
            _player2AssSprite.sortingOrder = 1;
            _player1ThornSprite.sortingOrder = 1;
            _player2ThornSprite.sortingOrder = 1;
        }
    }
}