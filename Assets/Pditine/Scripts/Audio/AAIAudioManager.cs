using System;
using System.Collections.Generic;
using System.Linq;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;

namespace Pditine.Audio
{
    public class AAIAudioManager : DdolSingletonMono<AAIAudioManager>
    {
        [SerializeField] private List<AudioClip> effectAudios = new();
        [SerializeField] private List<AudioClip> BGMAudios = new();
        private AudioClip _currentBGM;
        public AudioClip CurrentBGM => _currentBGM;

        private void Start()
        {
            PlayBGM("障碍地图背景音乐");
        }

        public void PlayBGM(string audioName)
        {
            if (_currentBGM&& audioName == _currentBGM.name) return;
            AudioClip theAudio = BGMAudios.FirstOrDefault(audio => audio.name == audioName);
            _currentBGM = theAudio;
            AudioSystem.PlayBGM(theAudio);
        }

        public void PlayEffect(string audioName)
        {
            AudioClip theAudio = effectAudios.FirstOrDefault(audio => audio.name == audioName);
            AudioSystem.PlayEffect(theAudio,transform);
        }
    }
}