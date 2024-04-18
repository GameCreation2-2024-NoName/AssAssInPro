using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PurpleFlowerCore.Utility
{
    public static class DelayUtility
    {
        public static void Delay(float time,UnityAction action,bool canScale = true)
        {
            MonoSystem.Start_Coroutine(DoDelay(time, action,canScale));
        }

        private static IEnumerator DoDelay(float time,UnityAction action,bool canScale)
        {
            float waitTime = canScale ? time * Time.timeScale : time;
            yield return new WaitForSeconds(waitTime);
            action?.Invoke();
        }
    }
}