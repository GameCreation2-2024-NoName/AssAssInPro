using System;
using Cinemachine;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;

namespace Pditine.GamePlay.Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] protected CinemachineVirtualCamera mainCamera;
        [SerializeField] protected CinemachineVirtualCamera player1Camera;
        [SerializeField] protected CinemachineVirtualCamera player2Camera;
        
        [SerializeField]private float motionTime;

        private bool _gameOver;
        public static CameraManager Instance { get; private set; }

        protected void Awake()
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
            EventSystem.AddEventListener("GameOver",SetGameOver);
        }

        private void OnDisable()
        {
            EventSystem.RemoveEventListener("GameOver",SetGameOver);
        }

        public void OnCollidePLayerAss(int id)
        {
            if (_gameOver) return;
            Time.timeScale = 0.3f;
            var playerCamera = id == 1 ? player1Camera : player2Camera;
            mainCamera.VirtualCameraGameObject.SetActive(false);
            playerCamera.VirtualCameraGameObject.SetActive(true);
            DelayUtility.Delay(motionTime, () =>
            {
                if (!_gameOver)
                {
                    mainCamera.VirtualCameraGameObject.SetActive(true);
                    playerCamera.VirtualCameraGameObject.SetActive(false);
                }
                Time.timeScale = 1;
            },false);
        }

        private void SetGameOver()
        {
            _gameOver = true;   
        }
    }
}