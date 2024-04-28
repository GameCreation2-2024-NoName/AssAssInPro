using System;
using PurpleFlowerCore;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Scripts
{
    public class InfoUI : MonoBehaviour
    {
        private float _target = 1;
        [SerializeField] private Slider theSlider;
        public Slider TheSlider => theSlider;
        [SerializeField][Range(0,1)] private float speed=0.15f;

        private void FixedUpdate()
        {
            if (!theSlider.value.Equals(_target))
                theSlider.value = Mathf.Lerp(theSlider.value, _target, speed);
        }

        public void SetValue(float value)
        {
            _target = value;
        }
        
    }
}