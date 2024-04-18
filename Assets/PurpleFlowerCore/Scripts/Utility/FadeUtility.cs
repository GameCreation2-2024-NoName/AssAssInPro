using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PurpleFlowerCore.Utility
{
    public class FadeUtility : SafeSingleton<FadeUtility>
    {
        public static void FadeOut(Graphic graphic,float speed, UnityAction allBack = null, float alpha = 1)
        {
            MonoSystem.Start_Coroutine(DoFadeOut(graphic,speed,allBack,alpha));
        }

        private static IEnumerator DoFadeOut(Graphic graphic,float speed, UnityAction allBack,float alpha)
        {
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, alpha);
            graphic.enabled = true;
            while (graphic.color.a>0.05f)
            {
                yield return new WaitForSeconds(1/speed);
                graphic.color -= new Color(0, 0, 0, 0.01f);
            }
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, 0);
            graphic.enabled = false;
            allBack?.Invoke();
        }
        
        public static void FadeOutTo(Graphic graphic,float speed, UnityAction allBack = null, float alpha = 1)
        {
            MonoSystem.Start_Coroutine(DoFadeOutTo(graphic,speed,allBack,alpha));
        }

        private static IEnumerator DoFadeOutTo(Graphic graphic,float speed, UnityAction allBack,float alpha)
        {
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, 1);
            graphic.enabled = true;
            while (graphic.color.a>alpha+0.05f)
            {
                yield return new WaitForSeconds(1/speed);
                graphic.color -= new Color(0, 0, 0, 0.01f);
            }
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, alpha);
            allBack?.Invoke();
        }
        
        public static void FadeOut(SpriteRenderer graphic,float speed, UnityAction allBack = null, float alpha = 1)
        {
            MonoSystem.Start_Coroutine(DoFadeOut(graphic,speed,allBack,alpha));
        }

        private static IEnumerator DoFadeOut(SpriteRenderer graphic,float speed, UnityAction allBack,float alpha)
        {
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, alpha);
            graphic.enabled = true;
            while (graphic.color.a>0.05f)
            {
                yield return new WaitForSeconds(1/speed);
                graphic.color -= new Color(0, 0, 0, 0.01f);
            }
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, 0);
            graphic.enabled = false;
            allBack?.Invoke();
        }
        
        public static void FadeIn(Graphic graphic,float speed, UnityAction allBack = null, float alpha = 1)
        {
            MonoSystem.Start_Coroutine(DoFadeIn(graphic,speed,allBack,alpha));
        }

        private static IEnumerator DoFadeIn(Graphic graphic,float speed, UnityAction allBack, float alpha)
        {
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, 0);
            graphic.enabled = true;
            while (graphic.color.a<alpha-0.05f)
            {
                yield return new WaitForSeconds(1/speed);
                graphic.color += new Color(0, 0, 0, 0.01f);
            }
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, alpha);
            graphic.enabled = false;
            allBack?.Invoke();
        }
        public static void FadeInAndStay(Graphic graphic,float speed, UnityAction allBack = null, float alpha = 1)
        {
            MonoSystem.Start_Coroutine(DoFadeInAndStay(graphic,speed,allBack,alpha));
        }

        private static IEnumerator DoFadeInAndStay(Graphic graphic,float speed, UnityAction allBack, float alpha)
        {
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, 0);
            graphic.enabled = true;
            while (graphic.color.a<alpha - 0.05f)
            {
                yield return new WaitForSeconds(1/speed);
                graphic.color += new Color(0, 0, 0, 0.01f);
            }
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, alpha);
            allBack?.Invoke();
        }
        
        public static void FadeInAndStay(SpriteRenderer graphic,float speed, UnityAction allBack = null, float alpha = 1)
        {
            MonoSystem.Start_Coroutine(DoFadeInAndStay(graphic,speed,allBack,alpha));
        }

        private static IEnumerator DoFadeInAndStay(SpriteRenderer graphic,float speed, UnityAction allBack, float alpha)
        {
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, 0);
            graphic.enabled = true;
            while (graphic.color.a<alpha - 0.05f)
            {
                yield return new WaitForSeconds(1/speed);
                graphic.color += new Color(0, 0, 0, 0.01f);
            }
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, alpha);
            allBack?.Invoke();
        }
    }
}