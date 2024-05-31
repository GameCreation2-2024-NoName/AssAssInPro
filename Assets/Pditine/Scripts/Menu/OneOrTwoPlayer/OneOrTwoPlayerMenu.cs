using Pditine.Data;
using Pditine.Data.GameModule;
using UnityEngine;

namespace Pditine.Menu.OneOrTwoPlayer
{
    public class OneOrTwoPlayerMenu : MonoBehaviour
    {
        public void SetGameModel(GameModelBase gameModel)
        {
            DataManager.Instance.PassingData.currentGameModel = gameModel;
        }
    }
}