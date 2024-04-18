using System;
using System.Collections;
using UnityEngine;
using PurpleFlowerCore.Base;

namespace PurpleFlowerCore
{
    public class MonoSystem: MonoBehaviour
    {
        private Action _updateEvent;
        private Action _lateUpdateEvent;
        private Action _fixedUpdateEvent;
     
        private static MonoSystem _instance;

        private static MonoSystem Instance
        {
            get
            {
                if (_instance is not null) return _instance;
                var root = new GameObject("Mono")
                {
                    transform =
                    {
                        parent = PFCManager.Instance.transform
                    }
                };
                _instance = root.AddComponent<MonoSystem>();
                return _instance;
            }
        }
        
        private void Update()
            => _updateEvent?.Invoke();
        
        private void LateUpdate()
            => _lateUpdateEvent?.Invoke();
        
        private void FixedUpdate()
            => _fixedUpdateEvent?.Invoke();
        
        
        /// <summary>
        /// 添加Update监听
        /// </summary>
        /// <param name="action"></param>
        public static void AddUpdateListener(Action action)
            => Instance._updateEvent += action;
        

        /// <summary>
        /// 移除Update监听
        /// </summary>
        /// <param name="action"></param>
        public static void RemoveUpdateListener(Action action)
            => Instance._updateEvent -= action;
        

        /// <summary>
        /// 添加LateUpdate监听
        /// </summary>
        /// <param name="action"></param>
        public static void AddLateUpdateListener(Action action)
            => Instance._lateUpdateEvent += action;
        

        /// <summary>
        /// 移除LateUpdate监听
        /// </summary>
        /// <param name="action"></param>
        public static void RemoveLateUpdateListener(Action action)
            => Instance._lateUpdateEvent -= action;
        

        /// <summary>
        /// 添加FixedUpdate监听
        /// </summary>
        /// <param name="action"></param>
        public static void AddFixedUpdateListener(Action action)
            => Instance._fixedUpdateEvent += action;
        

        /// <summary>
        /// 移除FixedUpdate监听
        /// </summary>
        /// <param name="action"></param>
        public static void RemoveFixedUpdateListener(Action action)
            => Instance._fixedUpdateEvent -= action;
        

        public static Coroutine Start_Coroutine(IEnumerator enumerator)
            => Instance.StartCoroutine(enumerator);

        public static void Stop_Coroutine(Coroutine coroutine)
            => Instance.StopCoroutine(coroutine);
        
    }
}