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
        // [SerializeField] private MMF_Player bornFeedback => lightBallRef.;
        private MMF_Player EatenFeedback => lightBallRef.Eaten;
        private MMF_Player AppearFeedback =>lightBallRef.Appear;
        private ParticleSystem Halo => lightBallRef.Halo;
        private GameObject TheBall => lightBallRef.TheBall;
        private ParticleSystem Spark => lightBallRef.Spark;
        //[SerializeField] private ParticleSystem halo;
        [SerializeField] private LightBallRef lightBallRef;
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
            // bornFeedback.Initialization();
            AppearFeedback.Initialization();
            EatenFeedback.Initialization();
            TheBall.gameObject.SetActive(false);
            Halo.Play();
            DelayUtility.Delay(1f, () =>
            {
                _ready = true;
                TheBall.gameObject.SetActive(true);
                //bornFeedback.PlayFeedbacks();
                AppearFeedback.PlayFeedbacks();
                Spark.Play();
            });

        }
        private void BeDestroy()
        {
            EatenFeedback.PlayFeedbacks();
            TheBall.SetActive(false);
            DelayUtility.Delay(0.5f, () =>
            {
                Destroy(gameObject);// todo:对象池
            });
        }
        
        //protected abstract BuffInfo AddBuff(PlayerController targetPlayer);
    }
}