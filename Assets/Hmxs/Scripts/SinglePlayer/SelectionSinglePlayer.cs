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

        private void StartGame()
        {
            PFCLog.Info("场景转换");
            SceneSystem.LoadScene(DataManager.Instance.PassingData.currentGameModel.GetARandomScene());
        }
    }
}