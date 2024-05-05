using Pditine.Data;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.GamePlay.UI
{
    public class PausePanel : MonoBehaviour
    {
        public void BackToMainMenu()
        {
            DataManager.Instance.PassingData.mainMenuOpenedMenuIndex = 0;
            SceneSystem.LoadScene(0);
        }

        public void ReStart()
        {
            SceneSystem.LoadScene(DataManager.Instance.PassingData.currentGameModel.SceneID);
        }
    }
}