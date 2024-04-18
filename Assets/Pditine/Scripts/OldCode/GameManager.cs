using Cinemachine;
using LJH.Scripts.Utility;
using PurpleFlowerCore;
using PurpleFlowerCore.Event;
using PurpleFlowerCore.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LJH
{
    public class GameManager : SingletonMono<GameManager>
    {
        private bool _gameOver;
        [SerializeField] private Image blackCurtain;

        [SerializeField] private CinemachineVirtualCamera camera;
        [SerializeField] private TextMeshProUGUI playerDeadInfo;

        private void OnEnable()
        {
            EventSystem.AddEventListener("GameOver", GameOver);
            EventSystem.AddEventListener("GameStart", GameStart);
            FadeUtility.FadeOut(blackCurtain,80);
            Time.timeScale = 1;
            _gameOver = false;
        }

        private void OnDisable()
        {
            EventSystem.RemoveEventListener("GameOver", GameOver);
            EventSystem.RemoveEventListener("GameStart", GameStart);
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
        }

        private void GameStart()
        {
            
        }

        public void PlayerDead(Transform thePlayer,int playerIndex)
        {
            if(_gameOver)return;
            CameraMoveUtility.MoveAndZoomForever(camera,thePlayer.transform, 0.04f, 3);
            //camera.Follow = thePlayer;
            //camera.LookAt = thePlayer;
            FadeUtility.FadeInAndStay(playerDeadInfo,80);
            playerDeadInfo.text = $"Player{playerIndex} Died";
        }
    }
}
