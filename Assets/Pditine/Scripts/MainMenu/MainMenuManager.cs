using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        public void LoadScene(int index)
        {
            SceneSystem.LoadScene(index);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}