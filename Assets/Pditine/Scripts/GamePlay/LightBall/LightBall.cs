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
        private bool _ready;
        [SerializeField] private MMF_Player bornFeedback;
        [SerializeField] private MMF_Player eatenFeedback;
        [SerializeField] private GameObject theBall;
        [SerializeField] private ParticleSystem halo;
        private SpriteRenderer SpriteRenderer => GetComponent<SpriteRenderer>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_ready) return; 

            if (other.CompareTag("Thorn")|| other.CompareTag("Ass"))
            {
                BeDestroy();
                _ready = false;
                //BuffManager.Instance.AttachBuff(AddBuff(other.GetComponent<PlayerController>()));
                BuffManager.Instance.AttachBuff(new BuffInfo(buffData,gameObject,other.GetComponentInParent<PlayerController>()));
            }
        }

        public void Init()
        {
            
            bornFeedback.Initialization();
            eatenFeedback.Initialization();
            theBall.gameObject.SetActive(false);
            halo.Play();
            DelayUtility.Delay(1.3f, () =>
            {
                _ready = true;
                theBall.gameObject.SetActive(true);
                bornFeedback.PlayFeedbacks();
            });

        }
        private void BeDestroy()
        {
            eatenFeedback.PlayFeedbacks();
            
            DelayUtility.Delay(0.2f, () =>
            {
                Destroy(gameObject);// todo:对象池
            });
        }
        
        //protected abstract BuffInfo AddBuff(PlayerController targetPlayer);
    }
}