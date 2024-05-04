using System;
using Cinemachine;
using Hmxs.Scripts;
using LJH.Scripts.Utility;
using MoreMountains.Feedbacks;
using Pditine.Data;
using Pditine.GamePlay.UI;
using Pditine.Player;
using Pditine.Player.Ass;
using Pditine.Player.Thorn;
using Pditine.Scripts.Data.DatePassing;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;

namespace Pditine.ClassicGame
{
    public class ClassicGameManager : MonoBehaviour
    {
        private bool _gameOver;
        [Header("相机")]
        [SerializeField] private CinemachineVirtualCamera camera;

        [Header("UI控件")]
        // [SerializeField] private Image blackCurtain;
        [SerializeField] private MMF_Player startEffect;
        [Header("玩家")]
        [SerializeField] private PlayerController player1;
        [SerializeField] private PlayerController player2;
        
        private PassingData _passingData;

        public static ClassicGameManager Instance { get; private set; }

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

        private void Start()
        {
            _passingData = DataManager.Instance.PassingData;
            player1.OnChangeHP += CheckPlayerDead;
            player2.OnChangeHP += CheckPlayerDead;
            Init();
        }

        // private void OnEnable()
        // {
        //     EventSystem.AddEventListener("GameOver", GameOver);
        // }
        // private void OnDisable()
        // {
        //     EventSystem.RemoveEventListener("GameOver", GameOver);
        // }

        private void Init()
        {
            //组装玩家
            CreatePlayer(_passingData.player1AssID,_passingData.player1ThornID,player1);
            CreatePlayer(_passingData.player2AssID,_passingData.player2ThornID,player2);
            
            startEffect.PlayFeedbacks();
            
            Time.timeScale = 1;
            _gameOver = false;
            DelayUtility.Delay(5.5f,()=>
            {
                PlayerCanMove(true);
            }); 
            PlayerManager.Instance.SwitchMap("GamePlay");
        }

        private void CreatePlayer(int assID,int thornID,PlayerController thePlayer)
        {
            AssBase theAss = Instantiate(DataManager.Instance.GetAssData(assID).Prototype).GetComponent<AssBase>();
            ThornBase theThorn = Instantiate(DataManager.Instance.GetThornData(thornID).Prototype).GetComponent<ThornBase>();
            theAss.Init(thePlayer);
            theThorn.Init(thePlayer);
            thePlayer.Init(theThorn,theAss);
        }
        
        //临时
        private void CheckPlayerDead(int hp,int playerID)
        {
            if(_gameOver)return;
            if (hp > 0) return;
            _gameOver = true;
            Time.timeScale = 0.3f;
            var theLoser = playerID == 1 ? player1 : player2;
            var theWinner = playerID == 1 ? player2 : player1;
            CameraMoveUtility.MoveAndZoomForever(camera,theLoser.transform, 0.04f, 3);
            PlayerCanMove(false);
            theLoser.BeDestroy();
            DelayUtility.Delay(2, () => { UIManager.Instance.GameOver(theWinner); }); 
        }

        public void PlayerCanMove(bool canMove)
        {
            player1.CanMove = canMove;
            player2.CanMove = canMove;
        }
    }
}
