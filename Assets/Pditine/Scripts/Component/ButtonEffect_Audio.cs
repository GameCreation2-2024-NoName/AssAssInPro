using Pditine.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Pditine.Component
{
    public class ButtonEffect_Audio : MonoBehaviour,IPointerEnterHandler,IPointerDownHandler
    {
        [SerializeField] private string selectedAudioName = "选中按钮";
        [SerializeField] private string pressDownAudioName = "按下按钮";

        
        public void OnPointerEnter(PointerEventData eventData)
        {
            AAIAudioManager.Instance.PlayEffect(selectedAudioName);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            AAIAudioManager.Instance.PlayEffect(pressDownAudioName);
        }
    }
}