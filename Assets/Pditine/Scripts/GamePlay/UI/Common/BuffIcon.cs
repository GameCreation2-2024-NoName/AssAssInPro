using System;
using Pditine.GamePlay.Buff;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class BuffIcon : MonoBehaviour
    {
        [SerializeField] private Image theImage;
        [SerializeField] private Image durationTimeUI;
        [SerializeField] private TextMeshProUGUI currentStack;
        
        private BuffInfo _buffInfo;
        public BuffInfo BuffInfo => _buffInfo;

        private void Update()
        {
            if (_buffInfo.buffData.durationTime < 10000)
                durationTimeUI.fillAmount = _buffInfo.durationCounter / _buffInfo.buffData.durationTime;
            else
                durationTimeUI.fillAmount = 0;
            if (_buffInfo.currentStack > 0)
                currentStack.text = _buffInfo.currentStack.ToString();
            else currentStack.text = "";
        }

        public void Init(BuffInfo buffInfo)
        {
            _buffInfo = buffInfo;
            theImage.sprite = buffInfo.buffData.icon;
        }
    }
}