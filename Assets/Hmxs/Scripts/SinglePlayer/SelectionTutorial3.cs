using Pditine.Data;
using PurpleFlowerCore;
using UnityEngine;

namespace Hmxs.Scripts.SinglePlayer
{
    public class SelectionTutorial3 : MonoBehaviour
    {
        [SerializeField] private PlayerSelection player1;

        private void Start()
        {
            if (player1 == null)
            {
                Debug.LogError("Player1 is null");
                return;
            }

            player1.onConfirm += () =>
            {
                Debug.Log("Player1 Confirm");
                if (player1.IsReady) StartGame();
            };
        }

        private void StartGame()
        {
            PFCLog.Info("场景转换");
            SceneSystem.LoadScene(8);
        }
    }
}