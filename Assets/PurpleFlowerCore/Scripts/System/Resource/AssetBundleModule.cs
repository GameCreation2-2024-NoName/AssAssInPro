using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using Object = UnityEngine.Object;
namespace PurpleFlowerCore.Resource
{
    public class AssetBundleModule
    {
        private AssetBundle _mainAB;
        private AssetBundle MainAB
        {
            get
            {
                if (_mainAB is not null) return _mainAB;
                _mainAB = AssetBundle.LoadFromFile(PathUrl + MainABName);
                return _mainAB;
            }
        }

        private AssetBundleManifest _manifest;
        
        private AssetBundleManifest Manifest
        {
            get
            {
                if (_manifest is not null) return _manifest;
                _manifest = MainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                return _manifest;
            }   
        }
        
        private readonly Dictionary<string, AssetBundle> _abDic = new();

        private string PathUrl => Application.streamingAssetsPath + "/";

        private string MainABName
        {
            get
            {
#if UNITY_IOS
                return "IOS"
#elif UNITY_ANDROID
                return "Android"
#else
                return "PC";
#endif
            }
        }

        public AssetBundle LoadAssetBundle(string abName)
        {
            var dependencies = Manifest.GetAllDependencies(abName);
            AssetBundle tempAB;
            foreach (var dependency in dependencies)
            {
                if (_abDic.ContainsKey(dependency)) continue;
                tempAB = AssetBundle.LoadFromFile(PathUrl + dependency);
                _abDic.Add(dependency,tempAB);
            }

            if (!_abDic.ContainsKey(abName))
            {
                tempAB = AssetBundle.LoadFromFile(PathUrl + abName);
                _abDic.Add(abName,tempAB);
            }
            
            return _abDic[abName];
        }
        
        public void UnLoadAssetBundle(string abName)
        {
            if (_abDic.ContainsKey(abName))
            {
                _abDic[abName].Unload(false);
                _abDic.Remove(abName);
            }
            else
            {
                PFCLog.Warning($"There is no AssetBundle called {abName}");
            }
        }

        public void ClearAssetBundle()
        {
            AssetBundle.UnloadAllAssetBundles(false);
            _abDic.Clear();
            _manifest = null;
            _manifest = null;
        }
        
        public T LoadResource<T>(string abName, string resName) where T : Object
        {
            if (typeof(T) == typeof(GameObject))
            {
                return Object.Instantiate(LoadAssetBundle(abName).LoadAsset<T>(resName));
            }
            return LoadAssetBundle(abName).LoadAsset<T>(resName);
        }

        public object LoadResource(string abName, string resName,System.Type type)
        {
            if(typeof(GameObject) == type)
            {
                return Object.Instantiate(LoadAssetBundle(abName).LoadAsset(resName,type));
            }
            return LoadAssetBundle(abName).LoadAsset(resName, type);
        }
        
        public object LoadResource(string abName, string resName)
        {
            return LoadAssetBundle(abName).LoadAsset(resName);
        }

        public void LoadResourceAsync<T>(string abName, string resName,UnityAction<T> callBack) where T: Object
        {
            MonoSystem.Start_Coroutine(DoLoadResourceAsync(abName, resName, callBack));
        }

        private IEnumerator DoLoadResourceAsync<T>(string abName, string resName, UnityAction<T> callBack) where T: Object
        {
            var abr = LoadAssetBundle(abName).LoadAssetAsync<T>(resName);
            yield return abr;
            callBack?.Invoke(abr.asset as T);
        }
        
        public void LoadResourceAsync(string abName, string resName,UnityAction<Object> callBack)
        {
            MonoSystem.Start_Coroutine(DoLoadResourceAsync(abName, resName, callBack));
        }

        private IEnumerator DoLoadResourceAsync(string abName, string resName, UnityAction<Object> callBack)
        {
            var abr = LoadAssetBundle(abName).LoadAssetAsync(resName);
            yield return abr;
            callBack?.Invoke(abr.asset);
        }
        
        public void LoadResourceAsync(string abName, string resName,System.Type type,UnityAction<Object> callBack)
        {
            MonoSystem.Start_Coroutine(DoLoadResourceAsync(abName, resName,type, callBack));
        }

        private IEnumerator DoLoadResourceAsync(string abName, string resName,System.Type type, UnityAction<Object> callBack)
        {
            var abr = LoadAssetBundle(abName).LoadAssetAsync(resName,type);
            yield return abr;
            callBack?.Invoke(abr.asset);
        }
    }
}