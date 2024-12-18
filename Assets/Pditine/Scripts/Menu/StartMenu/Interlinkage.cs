using UnityEngine;

namespace Pditine.StartMenu
{
    public class InterLinkage : MonoBehaviour
    {
        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }
    }
}