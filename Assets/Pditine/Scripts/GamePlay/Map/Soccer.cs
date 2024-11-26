using MoreMountains.Feedbacks;
using Pditine.Audio;
using PurpleFlowerCore.Utility;
using UnityEngine;

namespace Pditine.Map
{
    public class Soccer : MonoBehaviour
    {
        private bool _hasTriggered;
        public bool HasTriggered => _hasTriggered;
        [SerializeField] private MMF_Player bornFeedback;
        [SerializeField] private MMF_Player eatenFeedback;
        [SerializeField] private GameObject theBall;
        [SerializeField] private ParticleSystem halo;
        [SerializeField] private string cheerAudioName = "进球时欢呼声";
        [SerializeField] private string whistlingAudioName = "进球后足球在中间生成时的哨声";
        [SerializeField] private CircleCollider2D theCollider;
        
        public void Init()
        {
            bornFeedback.Initialization();
            eatenFeedback.Initialization();
            theBall.gameObject.SetActive(false);
            theCollider.enabled = false;
            halo.Play();
            _hasTriggered = false;
            DelayUtility.Delay(1f, () =>
            {
                theBall.gameObject.SetActive(true);
                bornFeedback.PlayFeedbacks();
                AAIAudioManager.Instance.PlayEffect(whistlingAudioName);
                theCollider.enabled = true;
            });
        
        }
        public void Destroy()
        {
            _hasTriggered = true;
            AAIAudioManager.Instance.PlayEffect(cheerAudioName);
            eatenFeedback.PlayFeedbacks();
            theBall.SetActive(false);
            theCollider.enabled = false;
            DelayUtility.Delay(0.7f, () =>
            {
                Destroy(gameObject);// todo:对象池
            });
        }
    }
}