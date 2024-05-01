using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Scripts
{
    public class ReadyUI : MonoBehaviour
    {
        [SerializeField] private Sprite yellow;
        [SerializeField] private Sprite green;
        [SerializeField] private Image theImage;
        public void SetReady(bool isReady)
        {
            if (isReady)
                theImage.sprite = green;
            else
                theImage.sprite = yellow;
        }
    }
}