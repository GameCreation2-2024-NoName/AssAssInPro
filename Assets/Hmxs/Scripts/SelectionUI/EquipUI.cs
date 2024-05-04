using Pditine.Component;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Scripts
{
    public class EquipUI : MonoBehaviour
    {
        [SerializeField] private UGUIOutline theOutline;
        [SerializeField] private Material theMaterial;
        [SerializeField] private Image theImage;
        public Image TheImage => theImage;

        public void SetNativeSize()
        {
            theImage.SetNativeSize();
        }
        
        public Sprite Sprite
        {
            get => theImage.sprite;
            set => theImage.sprite = value;
        }

        public void IsSelected(bool isSelected)
        {
            if (isSelected) theOutline.Init();
            else theImage.material = theMaterial;
        }
    }
}