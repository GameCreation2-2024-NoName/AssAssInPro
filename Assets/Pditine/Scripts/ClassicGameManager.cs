using Cinemachine;
using LJH.Scripts.Player;
using LJH.Scripts.Utility;
using Pditine.Scripts.Data;
using Pditine.Scripts.Data.DatePassing;
using Pditine.Scripts.Player;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Pditine.Scripts
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
        // [Header("玩家出生点")]
        // [SerializeField] private Transform player1BirthPoint;
        // [SerializeField] private Transform player2BirthPoint;
        [Header("数据引用")] 
        [SerializeField] private PassingData passingData;
        
        
        private void Start()
        {
            Init();
        }

        private void OnEnable()
        {
            EventSystem.AddEventListener("GameOver", GameOver);
            FadeUtility.FadeOut(blackCurtain,80);
            Time.timeScale = 1;
            _gameOver = false;
        }

        private void OnDisable()
        {
            EventSystem.RemoveEventListener("GameOver", GameOver);
        }

        private void Init()
        {
            //todo:获取InputHandler
            
            //生成玩家
            
        }

        private GameObject CreatePlayer(int assID,int thornID,PlayerController thePlayer)
        {
            GameObject theAss = Instantiate(DataManager.Instance.GetAssData(assID).Prototype,
                thePlayer.transform.position,quaternion.identity,thePlayer.transform);
            GameObject theThorn = Instantiate(DataManager.Instance.GetThornData(assID).Prototype,
                thePlayer.transform.position,quaternion.identity,thePlayer.transform);
            return null;
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
    }
}
