using System;
using System.Reflection;

namespace PurpleFlowerCore
{
    /// <summary>
    /// ����ģʽ�Ļ���
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
    /// ����ģʽ�Ļ��࣬����ʾʵ�������˽�й��캯��
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
                        PFCLog.Error("�޷��õ������˽���޲ι��캯��");
                    return _instance;
                }
            }
        }
    }
}