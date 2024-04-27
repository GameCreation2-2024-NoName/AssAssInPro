using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace PurpleFlowerCore
{
    public static class SceneSystem
    {
        
        public static void LoadScene(string name, UnityAction callBack = null)
        {
            SceneManager.LoadScene(name);
            callBack?.Invoke();
        }
        
        public static void LoadScene(int index, UnityAction callBack = null)
        {
            SceneManager.LoadScene(index);
            callBack?.Invoke();
        }

        public static void LoadSceneAsync(string name, UnityAction callBack = null)
        {
            MonoSystem.Start_Coroutine(DoLoadSceneAsync(name, callBack));
        }

        private static IEnumerator DoLoadSceneAsync(string name,UnityAction callBack)
        {
            var ao = SceneManager.LoadSceneAsync(name);

            while (!ao.isDone)
            {
                //todo:加载进度
                yield return 0;
            }
            
            yield return ao;
            callBack?.Invoke();
        }
        
        public static void LoadSceneAsync(int index, UnityAction callBack = null)
        {
            MonoSystem.Start_Coroutine(DoLoadSceneAsync(index, callBack));
        }

        private static IEnumerator DoLoadSceneAsync(int index,UnityAction callBack)
        {
            var ao = SceneManager.LoadSceneAsync(index);
            while (!ao.isDone)
            {
                yield return 0;
            }
            yield return ao;
            callBack?.Invoke();
        }
    }
}