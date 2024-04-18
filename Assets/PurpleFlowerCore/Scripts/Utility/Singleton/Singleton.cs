using System;
using System.Reflection;

namespace PurpleFlowerCore.Utility
{
    /// <summary>
    /// 单例模式的基类
    /// </summary>
    public abstract class Singleton<T> where T : class, new()
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                return _instance ??= new T();
            }
        }
        
    }
    /// <summary>
    /// 单例模式的基类，请显示实现子类的私有构造函数
    /// </summary>
    public abstract class SafeSingleton<T> where T : class
    {
        private static T _instance;

        protected static readonly object LockObj = new object();
        public static T Instance
        {
            get
            {
                if (_instance is not null) return _instance;
                lock (LockObj)
                {
                    if (_instance is not null) return _instance;
                    Type type = typeof(T);
                    var info = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                        null, Type.EmptyTypes, null);
                    if (info is not null) _instance = info.Invoke(null) as T;
                    else
                        PFCLog.Error("无法得到子类的私有无参构造函数");
                    return _instance;
                }
            }
        }
    }
}