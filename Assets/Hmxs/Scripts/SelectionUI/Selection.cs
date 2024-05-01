using System;
using Pditine.Scripts.Data;
using Pditine.Utility;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Scripts
{
    public class Selection : MonoBehaviour
    {
        [SerializeField] private PlayerSelection player1;
        [SerializeField] private PlayerSelection player2;

        private void Start()
        {
            if (player1 == null)
            {
                Debug.LogError("Player1 is null");
                return;
            }
            if (player2 == null)
            {
                Debug.LogError("Player2 is null");
                return;
            }

            player1.onConfirm += () =>
            {
                Debug.Log("Player1 Confirm");
                if (player1.IsReady && player2.IsReady) StartGame();
            };
            player2.onConfirm += () =>
            {
                Debug.Log("Player2 Confirm");
                if (player1.IsReady && player2.IsReady) StartGame();
            };
        }

        private void StartGame()
        {
            SceneSystem.LoadScene(DataManager.Instance.PassingData.currentGameModel.SceneID);
        }
        
    }
}