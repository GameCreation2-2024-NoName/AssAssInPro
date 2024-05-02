using Pditine.MainMenu;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.StartMenu
{
    public class StartMenuManager : MonoBehaviour
    {
        public void StartGame()
        {
            MainMenuManager.Instance.ChangeMenu("OneOrTwoPlayer");
        }

        public void Setting()
        {
            
        }

        public void About()
        {
            
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}