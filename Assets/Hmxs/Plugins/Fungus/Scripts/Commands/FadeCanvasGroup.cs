using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Fungus
{
    [CommandInfo("UI",
        "Fade CanvasGroup",
        "Fades a CanvasGroup object")]
    public class FadeCanvasGroup : Command
    {
        [Tooltip("List of objects to be affected by the tween")]
        [SerializeField] protected List<CanvasGroup> targetCanvasGroups = new();

        [Tooltip("Type of tween easing to apply")]
        [SerializeField] protected LeanTweenType tweenType = LeanTweenType.easeOutQuad;

        [Tooltip("Wait until this command completes before continuing execution")]
        [SerializeField] protected BooleanData waitUntilFinished = new BooleanData(true);

        [Tooltip("Time for the tween to complete")]
        [SerializeField] protected FloatData duration = new FloatData(1f);

        [SerializeField] protected FloatData targetAlpha = new FloatData(1f);

        public override void OnEnter()
        {
            if (targetCanvasGroups.Count == 0)
            {
                Continue();
                return;
            }

            ApplyTween();

            if (!waitUntilFinished)
                Continue();
        }

        private void ApplyTween()
        {
            foreach (var target in targetCanvasGroups.Where(targetObject => targetObject != null))
            {
                if (Mathf.Approximately(duration, 0f))
                    target.alpha = targetAlpha.Value;
                else
                    LeanTween.alphaCanvas(target, targetAlpha, duration).setEase(tweenType);

            }

            if (waitUntilFinished)
                LeanTween.value(gameObject, 0f, 1f, duration).setOnComplete(Continue);
        }
    }
}