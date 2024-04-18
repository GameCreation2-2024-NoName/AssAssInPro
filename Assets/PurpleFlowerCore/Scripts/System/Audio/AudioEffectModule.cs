using PurpleFlowerCore.Pool;
using UnityEngine;
using UnityEngine.Events;

namespace PurpleFlowerCore.Audio
{
    public class AudioEffectModule : MonoBehaviour
                                           {
                                               private GameObjectPoolData _pool;
                                               private GameObjectPoolData Pool
                                               {
                                                   get
                                                   {
                                                       if (_pool is not null) return _pool;
                                                       _pool = new GameObjectPoolData(transform,ResourceSystem.LoadResource<GameObject>("PFCRes/AudioPlayer"));
                return _pool;
            }
        }

        public void Play(AudioClip clip,Transform parent,UnityAction finishCallBack)
        {
            AudioPlayer thePlayer = Pool.Pop().GetComponent<AudioPlayer>();
            if(parent)
            {
                thePlayer.transform.parent = parent;
                thePlayer.transform.position = parent.position;
            }

            finishCallBack += () =>
            {
                Pool.Push(thePlayer.gameObject);
            };
            thePlayer.Play(clip,1,finishCallBack);
        }
        
        public void Play(AudioClip clip,Vector3 position,UnityAction finishCallBack)
        {
            AudioPlayer thePlayer = Pool.Pop().GetComponent<AudioPlayer>();
            thePlayer.transform.position = position;
            
            finishCallBack += () =>
            {
                Pool.Push(thePlayer.gameObject);
            };
            thePlayer.Play(clip,1,finishCallBack);
        }
    }
}