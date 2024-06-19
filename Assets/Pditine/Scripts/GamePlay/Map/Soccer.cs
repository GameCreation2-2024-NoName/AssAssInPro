using MoreMountains.Feedbacks;
using Pditine.Audio;
using PurpleFlowerCore.Utility;
using UnityEngine;

namespace Pditine.Map
{
    public class Soccer : MonoBehaviour
    {
        private bool _ready;
        [SerializeField] private MMF_Player bornFeedback;
        [SerializeField] private MMF_Player eatenFeedback;
        [SerializeField] private GameObject theBall;
        [SerializeField] private ParticleSystem halo;
        [SerializeField] private string cheerAudioName = "进球时欢呼声";
        [SerializeField] private string whistlingAudioName = "进球后足球在中间生成时的哨声";
        
        public void Init()
        {
            
            bornFeedback.Initialization();
            eatenFeedback.Initialization();
            theBall.gameObject.SetActive(false);
            halo.Play();
            DelayUtility.Delay(1f, () =>
            {
                _ready = true;
                theBall.gameObject.SetActive(true);
                bornFeedback.PlayFeedbacks();
                AAIAudioManager.Instance.PlayEffect(whistlingAudioName);
            });
        
        }
        public void Destroy()
        {
            AAIAudioManager.Instance.PlayEffect(cheerAudioName);
            eatenFeedback.PlayFeedbacks();
            theBall.SetActive(false);
            DelayUtility.Delay(0.7f, () =>
            {
                Destroy(gameObject);// todo:对象池
            });
        }
    }
}