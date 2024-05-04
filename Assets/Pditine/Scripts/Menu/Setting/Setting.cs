using System;
using PurpleFlowerCore;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.Menu.Setting
{
    public class Setting : MonoBehaviour
    {
        [SerializeField] private Slider bgm;
        [SerializeField] private Slider effect;

        private void Start()
        {
            AudioSystem.BGMVolume = 0.7f;
            AudioSystem.EffectVolume = 0.7f;
            bgm.onValueChanged.AddListener(ChangeBGMVolume);
            effect.onValueChanged.AddListener(ChangeEffectVolume);
        }

        private void ChangeBGMVolume(float value)
        {
            AudioSystem.BGMVolume  = value;
        }

        private void ChangeEffectVolume(float value)
        {
            AudioSystem.EffectVolume = value;
        }
    }
}