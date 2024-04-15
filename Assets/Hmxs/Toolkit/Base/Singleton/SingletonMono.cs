using System;
using UnityEngine;

namespace Hmxs.Toolkit.Base.Singleton
{
    /// <summary>
    /// 泛型单例基类-继承Mono
    /// 采用Lazy进行实例化保证线程安全
    /// </summary>
    public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                _instance ??= new Lazy<GameObject>(new GameObject(typeof(T).Name)).Value.AddComponent<T>();
                return _instance;
            }
        }
    }
}