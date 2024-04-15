using System;
using UnityEngine;
using UnityEngine.UI;

namespace Hmxs.Toolkit.Flow.Timer
{
    public static class FadeTimer
    {
        public static void Fade(Graphic graphic, float duration, float startAlpha, float targetAlpha,
            AnimationCurve curve = null, bool useRealTime = false, Action onComplete = null)
        {
            if (graphic == null) return;
            curve ??= AnimationCurve.Linear(0f, 0f, 1f, 1f);
            var startColor = new Color(graphic.color.r, graphic.color.g, graphic.color.b, startAlpha);
            graphic.color = startColor;
            var targetColor = new Color(graphic.color.r, graphic.color.g, graphic.color.b, targetAlpha);
            graphic.gameObject.SetActive(true);
            Color tempColor;
            Timer.Register(
                duration: duration,
                onComplete: onComplete,
                onUpdate: time =>
                {
                    tempColor = Color.Lerp(startColor, targetColor, curve.Evaluate(time / duration));
                    graphic.color = tempColor;
                },
                useRealTime: useRealTime,
                timerID: "Fade"
            );
        }

        public static void Fade(Graphic graphic, float duration, Color startColor, Color targetColor,
            AnimationCurve curve = null, bool useRealTime = false, Action onComplete = null)
        {
            if (graphic == null) return;
            curve ??= AnimationCurve.Linear(0f, 0f, 1f, 1f);
            graphic.color = startColor;
            graphic.gameObject.SetActive(true);
            Color tempColor;
            Timer.Register(
                duration: duration,
                onComplete: onComplete,
                onUpdate: time =>
                {
                    tempColor = Color.Lerp(startColor, targetColor, curve.Evaluate(time / duration));
                    graphic.color = tempColor;
                },
                useRealTime: useRealTime,
                timerID: "Fade"
            );
        }
    }
}