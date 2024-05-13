﻿using System.Collections;
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
            yield return new WaitForSecondsRealtime(waitTime);
            action?.Invoke();
        }

        public static void DelayFrame(int frame, UnityAction action)
        {
            MonoSystem.Start_Coroutine(DoDelayFrame(frame, action));
        }

        private static IEnumerator DoDelayFrame(int frame,UnityAction action)
        {
            if(frame>0)
                for (int i = 1; i < frame; i++)
                {
                    yield return null;
                }
            action?.Invoke();
        }
    }
}