using Pditine.Data;
using Pditine.Data.GameModule;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.Menu.OneOrTwoPlayer
{
    public class OneOrTwoPlayerMenu : MonoBehaviour
    {
        [SerializeField] private GameModelBase theGameModel;
        public void StartSinglePlayerGame()
        {
            DataManager.Instance.PassingData.currentGameModel = theGameModel;
            SceneSystem.LoadScene(DataManager.Instance.PassingData.currentGameModel.GetARandomScene());
        }
    }
}