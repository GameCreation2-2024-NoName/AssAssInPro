using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Pditine.Utility
{
    public class SceneChangingEffectManager : SingletonMono<SceneChangingEffectManager>
    {
        [SerializeField] private CircleMask circleMask;
        [SerializeField] private Image blackCurtain;
        
        public void CircleCover(Vector3 pos,UnityAction callBack = null)
        {
            circleMask.Cover(pos,callBack);
            FadeUtility.FadeInAndStay(blackCurtain,60);
        }
        
    }
}