using System;
using Pditine.Data;
using PurpleFlowerCore;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs.Scripts.SinglePlayer
{
    public class SelectionSinglePlayer : MonoBehaviour
    {
        [Required] [SerializeField] private PlayerSelectionSingle player1;

        private void Start()
        {
            player1.onConfirm += () =>
            {
                if (player1.IsReady) StartGame();
            };
        }

        private void OnEnable()
        {
            PlayerManager.Instance.Reset();
        }

        private void StartGame()
        {
            PFCLog.Info("场景转换");
            SceneSystem.LoadScene(6);
        }
    }
}