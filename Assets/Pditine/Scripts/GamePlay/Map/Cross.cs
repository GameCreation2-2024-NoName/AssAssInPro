using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Pditine.Map
{
    public class Cross : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private List<CrossButton> buttons = new();
        private float _target = 0;

        private void Update()
        {
            if(_target>0)
            {
                float deltaAngle = speed * Time.deltaTime;
                _target -= deltaAngle;
                transform.Rotate(0, 0, deltaAngle);
            }
        }

        public void Trigger()
        {
            _target += 90;
            foreach (var button in buttons)
            {
                button.ResetCD();
            }
        }
    }
}