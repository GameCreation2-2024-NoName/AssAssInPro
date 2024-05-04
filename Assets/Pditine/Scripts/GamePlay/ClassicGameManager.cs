using Cinemachine;
using Hmxs.Scripts;
using LJH.Scripts.Utility;
using Pditine.Player;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
using Pditine.Scripts.Data;
using Pditine.Scripts.Data.DatePassing;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Pditine.ClassicGame
{
    public class ClassicGameManager : SingletonMono<ClassicGameManager>
    {
        private bool _gameOver;
        [Header("相机")]
        [SerializeField] private CinemachineVirtualCamera camera;
        [Header("UI控件")]
        [SerializeField] private Image blackCurtain;
        [SerializeField] private TextMeshProUGUI playerDeadInfo;
        [Header("玩家")]
        [SerializeField] private PlayerController player1;
        [SerializeField] private PlayerController player2;

        private PassingData _passingData;
        
        private void Start()
        {
            _passingData = DataManager.Instance.PassingData;
            Init();
            Time.timeScale = 1;
            _gameOver = false;
            FadeUtility.FadeOut(blackCurtain,80);
            PlayerManager.Instance.SwitchMap("GamePlay");
        }

        private void OnEnable()
        {
            EventSystem.AddEventListener("GameOver", GameOver);
        }
        private void OnDisable()
        {
            EventSystem.RemoveEventListener("GameOver", GameOver);
        }

        private void Init()
        {
            //组装玩家
            CreatePlayer(_passingData.player1AssID,_passingData.player1ThornID,player1);
            CreatePlayer(_passingData.player2AssID,_passingData.player2ThornID,player2);
            
            PlayerCanMove(true);
        }

        private void CreatePlayer(int assID,int thornID,PlayerController thePlayer)
        {
            AssBase theAss = Instantiate(DataManager.Instance.GetAssData(assID).Prototype).GetComponent<AssBase>();
            ThornBase theThorn = Instantiate(DataManager.Instance.GetThornData(thornID).Prototype).GetComponent<ThornBase>();
            theAss.Init(thePlayer);
            theThorn.Init(thePlayer);
            thePlayer.Init(theThorn,theAss);
        }
        
        private void GameOver()
        {
            if (_gameOver) return;
            _gameOver = true;
            Time.timeScale = 0.3f;
            FadeUtility.FadeInAndStay(
                blackCurtain,
                20,
                () => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); });
            //todo:游戏结束逻辑
        }
        
        public void PlayerDead(Transform thePlayer,int playerIndex)
        {
            if(_gameOver)return;
            //todo:换一种摄像机移动方式
            CameraMoveUtility.MoveAndZoomForever(camera,thePlayer.transform, 0.04f, 3);
            FadeUtility.FadeInAndStay(playerDeadInfo,80);
            playerDeadInfo.text = $"Player{playerIndex} Died";
        }

        public void PlayerCanMove(bool canMove)
        {
            player1.CanMove = canMove;
            player2.CanMove = canMove;
        }
    }
}
