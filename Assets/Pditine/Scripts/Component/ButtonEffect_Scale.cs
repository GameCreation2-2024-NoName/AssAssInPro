using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pditine.Component
{
    public class ButtonEffect_Scale : MonoBehaviour, IPointerEnterHandler,IPointerDownHandler,IPointerExitHandler,IPointerUpHandler
    {
        [SerializeField] private float maxScale = 1.1f;
        [SerializeField] private float minScale = 0.9f;
        [SerializeField][Range(0,1)] private float scalingSpeed = 0.15f;
        private float _target = 1;
        private float _currentScale = 1;
        private Vector3 _originalScale;
        private void Start()
        {
            _originalScale = transform.localScale;
        }

        private void FixedUpdate()
        {
            if(!_currentScale.Equals(_target))
            {
                _currentScale = Mathf.Lerp(_currentScale, _target, scalingSpeed);
                transform.localScale = _originalScale*_currentScale;
            }
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _target = maxScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _target = minScale;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _target = 1;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _target = maxScale;
        }
    }
}