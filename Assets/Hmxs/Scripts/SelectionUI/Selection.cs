using Pditine.Scripts.Data;
using PurpleFlowerCore;
using UnityEngine;

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
            PFCLog.Info("场景转换");
            SceneSystem.LoadScene(DataManager.Instance.PassingData.currentGameModel.SceneID);
        }
        
    }
}