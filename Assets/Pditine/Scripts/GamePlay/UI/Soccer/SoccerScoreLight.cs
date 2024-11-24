// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_11_24
// -------------------------------------------------

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class SoccerScoreLight : MonoBehaviour
    {
        [SerializeField] private Image scoreLight;
        [SerializeField] private Sprite scoreLightOn;
        [SerializeField] private Sprite scoreLightOff;
        public void Switch(bool isOn)
        {
            scoreLight.sprite = isOn ? scoreLightOn : scoreLightOff;
            scoreLight.SetNativeSize();
        }
    }
}