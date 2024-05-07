using MoreMountains.Feedbacks;
using Pditine.GamePlay.Buff;
using Pditine.Player;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pditine.GamePlay.LightBall
{
    //public abstract class LightBallBase : MonoBehaviour
    public class LightBall : MonoBehaviour
    {
        [SerializeField] private BuffData buffData;
        private bool _hasTriggered;
        
        [SerializeField] private MMF_Player bornFeedback;
        [SerializeField] private MMF_Player eatenFeedback;
        private SpriteRenderer SpriteRenderer => GetComponent<SpriteRenderer>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_hasTriggered) return; 

            if (other.CompareTag("Thorn")|| other.CompareTag("Ass"))
            {
                BeDestroy();
                _hasTriggered = true;
                //BuffManager.Instance.AttachBuff(AddBuff(other.GetComponent<PlayerController>()));
                BuffManager.Instance.AttachBuff(new BuffInfo(buffData,gameObject,other.GetComponentInParent<PlayerController>()));
            }
        }

        public void Init()
        {
            //SpriteRenderer.sprite = buffData.icon;
            bornFeedback.Initialization();
            eatenFeedback.Initialization();
            _hasTriggered = false;
            bornFeedback.PlayFeedbacks();
            //FadeUtility.FadeInAndStay(SpriteRenderer,100);
        }
        private void BeDestroy()
        {
            eatenFeedback.PlayFeedbacks();
            DelayUtility.Delay(0.2f, () => { Destroy(gameObject); });
        }
        
        //protected abstract BuffInfo AddBuff(PlayerController targetPlayer);
    }
}