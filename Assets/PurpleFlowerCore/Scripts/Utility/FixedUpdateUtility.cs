using System;
using UnityEngine;
using UnityEngine.Events;

namespace PurpleFlowerCore.Utility
{
    public class FixedUpdateUtility : MonoBehaviour
    {
        private float _currentDeltaTime;
        private float _deltaTime;
        private UnityAction _action;
        private bool _canScale;
        private void Update()
        {
            _currentDeltaTime += Time.deltaTime;
            float theDeltaTime = _canScale ? _deltaTime * Time.timeScale : _deltaTime;
            if (_currentDeltaTime >= theDeltaTime)
            {
                _action?.Invoke();
                _currentDeltaTime = 0;
            }
        }

        public static void FixedUpdate_(float deltaTime,UnityAction action,bool canScale)
        {
            var theFixedUpdate = new GameObject("FixedUpdate_" + deltaTime).AddComponent<FixedUpdateUtility>();
            theFixedUpdate._deltaTime = deltaTime;
            theFixedUpdate._action = action;
            theFixedUpdate._canScale = canScale;
        }
        
    }
}