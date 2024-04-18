using UnityEngine;
using UnityEngine.Events;

namespace PurpleFlowerCore.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField]private AudioSource audioSource;
        public AudioSource AudioSource => audioSource;
        
        public event UnityAction FinishCallBack;

        public void Play(AudioClip clip,float volume,UnityAction finishCallBack)
        {
            FinishCallBack += finishCallBack;
            audioSource.clip = clip;
            audioSource.volume = volume;
            Invoke(nameof(PlayFinished),clip.length);
            audioSource.Play();
        }

        private void Mute()
        {
            audioSource.mute = true;
        }

        private void UnMute()
        {
            audioSource.mute = false;
        }

        private void PlayFinished()
        {
            FinishCallBack?.Invoke();
            FinishCallBack = null;
        }
    }
}