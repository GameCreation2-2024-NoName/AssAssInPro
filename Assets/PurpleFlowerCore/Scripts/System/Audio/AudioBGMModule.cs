using System;
using UnityEngine;

namespace PurpleFlowerCore.Audio
{
    public class AudioBGMModule : MonoBehaviour
    {
        private AudioSource _bgmSource;

        private AudioSource BgmSource
        {
            get
            {
                if (_bgmSource is not null) return _bgmSource;
                _bgmSource = gameObject.AddComponent<AudioSource>();
                _bgmSource.loop = true;
                return _bgmSource;
            }
        }

        public float Volume
        {
            get => BgmSource.volume;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value));
                BgmSource.volume = value;
            }
        }

        public bool Mute
        {
            get => BgmSource.mute;
            set => BgmSource.mute = value;
        }

        public void PlayBGM(AudioClip clip)
        {
            BgmSource.clip = clip;
            BgmSource.Play();
        }

        public void Pause()
        {
            BgmSource.Pause();
        }

        public void Unpause()
        {
            BgmSource.Play();
        }
        
    }
}