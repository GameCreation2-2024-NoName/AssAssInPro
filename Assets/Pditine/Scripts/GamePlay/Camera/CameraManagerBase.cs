using System;
using Cinemachine;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pditine.GamePlay.Camera
{
    public class CameraManagerBase : MonoBehaviour
    {
        [SerializeField] protected CinemachineVirtualCamera mainCamera;
        [SerializeField] protected CinemachineVirtualCamera player1Camera;
        [SerializeField] protected CinemachineVirtualCamera player2Camera;
        
        [SerializeField]private float motionTime;
        public bool collideEffectOn = true;

        private bool _gameOver;
        public static CameraManagerBase Instance { get; private set; }

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

        public virtual void OnCollidePLayerAss(int id)
        {
            if (!collideEffectOn) return;
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