using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.StartMenu
{
    public class StartMenuManager : MonoBehaviour
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