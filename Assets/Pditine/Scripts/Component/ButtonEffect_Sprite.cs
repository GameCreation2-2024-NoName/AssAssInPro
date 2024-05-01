using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pditine.Component
{
    public class ButtonEffect_Sprite : MonoBehaviour,ISelectHandler,IDeselectHandler,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField] private Sprite normal;
        [SerializeField] private Sprite selected;
        [SerializeField] private Image theImage;
        private void Start()
        {
            theImage.sprite = normal;
        }

        public void OnSelect(BaseEventData eventData)
        {
            theImage.sprite = selected;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            theImage.sprite = normal;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            theImage.sprite = selected;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            theImage.sprite = normal;
        }
    }
}