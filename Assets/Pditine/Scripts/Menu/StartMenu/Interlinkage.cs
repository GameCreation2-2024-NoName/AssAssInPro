using UnityEngine;

namespace Pditine.StartMenu
{
    public class InterLinkage : MonoBehaviour
    {
        [SerializeField] private string url;

        public void OpenURL()
        {
            Application.OpenURL(url);
        }
    }
}