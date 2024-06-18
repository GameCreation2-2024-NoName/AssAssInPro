using MoreMountains.Feedbacks;
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
        [SerializeField] private Transform createPiont;
        
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
            });
        
        }
        public void Destroy()
        {
            eatenFeedback.PlayFeedbacks();
            theBall.SetActive(false);
            DelayUtility.Delay(0.7f, () =>
            {
                Destroy(gameObject);// todo:对象池
            });
        }
    }
}