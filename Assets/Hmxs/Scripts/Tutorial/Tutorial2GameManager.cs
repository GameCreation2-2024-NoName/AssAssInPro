using System;
using Hmxs.Toolkit.Plugins.Fungus.FungusTools;
using Pditine.Data;
using Pditine.GamePlay.Buff;
using Pditine.GamePlay.GameManager;
using Pditine.GamePlay.LightBall;
using Pditine.GamePlay.UI;
using Pditine.Player;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hmxs.Scripts.Tutorial
{
    public class Tutorial2GameManager : GameManagerBase<Tutorial2GameManager>
    {
        public Transform Player1 => player1.transform;
        public PlayerController Player1Controller => player1;

        [Required] [SerializeField] private GameObject player1Thorn;
        [Required] [SerializeField] private GameObject player1Ass;
        [Required] [SerializeField] private GameObject player2Thorn;
        [Required] [SerializeField] private GameObject player2Ass;

        [Required] [SerializeField] private Canvas aiHeartCanvas;
        [Required] [SerializeField] private Canvas playerBuffCanvas;

        [Title("LightBall")]
        [Required] [SerializeField] private GameObject lightBall;
        [Required] [SerializeField] private DetectPoint detectPoint;

        private SpriteRenderer _lightBallSprite;

        private SpriteRenderer _player1ThornSprite;
        private SpriteRenderer _player1AssSprite;
        private SpriteRenderer _player2ThornSprite;
        private SpriteRenderer _player2AssSprite;

        protected override void Update()
        {
            // Disable Pause
        }

        protected override void Init()
        {
            DataManager.Instance.PassingData.player1AssID = player1Ass.GetComponent<AssBase>().Data.ID;
            DataManager.Instance.PassingData.player1ThornID = player1Thorn.GetComponent<ThornBase>().Data.ID;
            DataManager.Instance.PassingData.player2AssID = player2Ass.GetComponent<AssBase>().Data.ID;
            DataManager.Instance.PassingData.player2ThornID = player2Thorn.GetComponent<ThornBase>().Data.ID;

            _player1AssSprite = player1Ass.GetComponent<SpriteRenderer>();
            _player1ThornSprite = player1Thorn.GetComponent<SpriteRenderer>();
            _player2AssSprite = player2Ass.GetComponent<SpriteRenderer>();
            _player2ThornSprite = player2Thorn.GetComponent<SpriteRenderer>();

            _lightBallSprite = lightBall.GetComponentInChildren<SpriteRenderer>();

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

            FlowchartManager.ExecuteBlock("Tutorial2Start");
        }

        public void EnablePlayerInput()
        {
            PlayerManager.Instance.SwitchMap("GamePlay");
            player1.canMove = true;
            player2.canMove = true;
        }

        public void BanPlayerInput()
        {
            player1.canMove = false;
            player2.canMove = false;
        }

        public void PlayStartFeedback() => startEffect.PlayFeedbacks();

        public void SetHighlight(SpriteRenderer sprite) => sprite.sortingOrder = 999;
        public void SetHighlight(Canvas canvas) => canvas.sortingOrder = 999;

        public void ExecuteBlock(string blockName) => FlowchartManager.ExecuteBlock(blockName);

        public void ResetHighlight()
        {
            _lightBallSprite.sortingOrder = 0;
            _player1AssSprite.sortingOrder = 1;
            _player2AssSprite.sortingOrder = 1;
            _player1ThornSprite.sortingOrder = 1;
            _player2ThornSprite.sortingOrder = 1;
            aiHeartCanvas.sortingOrder = aiHeartCanvas.rootCanvas.sortingOrder;
            playerBuffCanvas.sortingOrder = playerBuffCanvas.rootCanvas.sortingOrder;
        }

        public void SetPlayer1Movable(bool movable) => player1.canMove = movable;
        public void SetPlayer2Movable(bool movable) => player2.canMove = movable;

        public void SpawnLightBall()
        {
            lightBall.SetActive(true);
            while (detectPoint.HasObjectAround())
            {
                lightBall.transform.position += new Vector3(Random.value, Random.value, 0);
            }
            lightBall.GetComponent<LightBall>().Init();
        }
    }
}