using UnityEngine;

namespace PurpleFlowerCore
{

    public abstract class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
    {
        public static T Instance;
        protected virtual void Awake()
        {
            if (Instance is null)
            {
                Instance = this as T;
            }
            else
            {
                //Destroy(this);
                PFCLog.Warning("单例重复挂载,物体:"+gameObject.name);
            }
        }
    }

    public abstract class DdolSingletonMono<T> : MonoBehaviour where T : DdolSingletonMono<T>
    {
        public static T Instance;
        protected virtual void Awake()
        {
            if (Instance is null)
            {
                Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
                PFCLog.Warning("单例重复挂载,物体:"+gameObject.name);
            }
        }
    }
    
    public abstract class AutoSingletonMono<T> : MonoBehaviour where T : AutoSingletonMono<T>
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance is not null) return _instance;
                var theGameObject = new GameObject(typeof(T).Name);
                _instance = theGameObject.AddComponent<T>();
                return _instance;
            }
        }
    }
}