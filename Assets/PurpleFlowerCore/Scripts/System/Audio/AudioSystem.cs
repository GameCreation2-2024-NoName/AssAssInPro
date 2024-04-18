using System.Collections.Generic;
using System.IO;
using PurpleFlowerCore.Audio;
using PurpleFlowerCore.Base;
using UnityEngine;
using UnityEngine.Events;

namespace PurpleFlowerCore
{
    public static class AudioSystem
    {
        private static GameObject _root;

        private static GameObject Root
        {
            get
            {
                if (_root is not null) return _root;
                _root = new GameObject("Audio")
                {
                    transform = { parent = PFCManager.Instance.transform }
                };
                return _root;
            }
        }
        //todo:使用某种方法管理音频文件
        #region BGM

        private static AudioBGMModule _bgmModule;

        public static AudioBGMModule BGMModule
        {
            get
            {
                if (_bgmModule is not null) return _bgmModule;
                _bgmModule = Root.AddComponent<AudioBGMModule>();
                return _bgmModule;
            }
        }
        
        public static float BGMVolume
        {
            get => BGMModule.Volume;
            set => BGMModule.Volume = value;
            
        }

        public static bool BGMMute
        {
            get => BGMModule.Mute;
            set => BGMModule.Mute = value;
        }

        public static void PlayBGM(AudioClip clip)
        {
            BGMModule.PlayBGM(clip);
        }

        public static void PauseBGM()
        {
            BGMModule.Pause();
        }

        public static void UnpauseBGM()
        {
            BGMModule.Unpause();
        }
        
        #endregion

        #region Effect

        private static Dictionary<string, AudioClip> _clips;

        // private static Dictionary<string, AudioClip> Clips
        // {
        //     get
        //     {
        //         if (_clips is not null) return _clips;
        //         var a = Directory.GetFiles("");
        //     }
        // }
        
        private static AudioEffectModule _effectModule;

        public static AudioEffectModule EffectModule
        {
            get
            {
                if (_effectModule is not null) return _effectModule;
                _effectModule = Root.AddComponent<AudioEffectModule>();
                return _effectModule;
            }
        }
        
        public static void PlayEffect(AudioClip clip,Transform parent = null,UnityAction finishCallBack = null)
        {
            EffectModule.Play(clip,parent,finishCallBack);
        }
        
        public static void PlayEffect(AudioClip clip,Vector3 position = default,UnityAction finishCallBack = null)
        {
            EffectModule.Play(clip,position,finishCallBack);
        }

        public static void PlayEffect(string clipName,Transform parent = null,UnityAction finishCallBack = null)
        {
            
        }
        
        public static void PlayEffect(string clipName,Vector3 position = default,UnityAction finishCallBack = null)
        {
            
        }
        
        #endregion
    }
}