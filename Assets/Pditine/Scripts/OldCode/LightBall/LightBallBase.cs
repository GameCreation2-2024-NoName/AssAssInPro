using System;
using Hmxs.Toolkit.Flow.Timer;
using LJH.Scripts.Player;
using MoreMountains.Feedbacks;
using Pditine.Scripts.Player;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LJH.LightBall
{
    public abstract class LightBallBase : MonoBehaviour
    {
        [SerializeField]protected bool HasTriggered;

        [Title("Feedback")]
        [SerializeField] private MMF_Player bornFeedback;
        [SerializeField] private MMF_Player eatenFeedback;
        protected SpriteRenderer SpriteRenderer => GetComponent<SpriteRenderer>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (HasTriggered) return; 

            if (other.CompareTag("Thorn")|| other.CompareTag("Ass"))
            {
                PFCLog.Info("碰撞:"+gameObject.name);
                AddBuff(other.GetComponentInParent<PlayerController>());
                BeDestroy();
                HasTriggered = true;
            }
        }
        protected abstract void AddBuff(PlayerController thePlayer);

        public virtual void BeCreate()
        {
            bornFeedback.Initialization();
            eatenFeedback.Initialization();
            HasTriggered = false;
            bornFeedback.PlayFeedbacks();
            //FadeUtility.FadeInAndStay(SpriteRenderer,100);
        }

        protected virtual void BeDestroy()
        {
            eatenFeedback.PlayFeedbacks();
            DelayUtility.Delay(0.2f, () => { Destroy(gameObject); });
            // FadeUtility.FadeOut(SpriteRenderer,200, () =>
            // {
            //     PoolSystem.PushGameObject(gameObject);
            // });
        }
    }
}