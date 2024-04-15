using UnityEngine;

namespace Hmxs.Toolkit.Base.Singleton
{
    /// <summary>
    /// 泛型单例基类-继承ScriptableObject
    /// </summary>
    public abstract class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
    {
        private static T _instance;
        
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var assets = Resources.LoadAll<T>($"");
                    if (assets == null || assets.Length < 1)
                        throw new System.Exception($"SingletonSO: Null {typeof(T)} SO instance in Resources directory");
                    if (assets.Length > 1)
                        Debug.LogWarning($"SingletonSO: Multiple {typeof(T)} SO instance in Resources directory");
                    _instance = assets[0];
                }
                return _instance;
            }
        }
    }
}