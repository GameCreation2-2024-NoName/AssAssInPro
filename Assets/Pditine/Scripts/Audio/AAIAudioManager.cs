using System;
using System.Collections.Generic;
using System.Linq;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.Audio
{
    public class AAIAudioManager : DdolSingletonMono<AAIAudioManager>
    {
        [SerializeField] private List<AudioClip> effectAudios = new();
        [SerializeField] private List<AudioClip> BGMAudios = new();

        private void Start()
        {
            PlayBGM("背景音乐");
        }

        public void PlayBGM(string audioName)
        {
            AudioClip theAudio = BGMAudios.FirstOrDefault(audio => audio.name == audioName);
            AudioSystem.PlayBGM(theAudio);
        }

        public void PlayEffect(string audioName)
        {
            AudioClip theAudio = effectAudios.FirstOrDefault(audio => audio.name == audioName);
            AudioSystem.PlayEffect(theAudio,transform);
        }
    }
}