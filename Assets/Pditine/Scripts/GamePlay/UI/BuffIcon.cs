using System;
using Pditine.GamePlay.Buff;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class BuffIcon : MonoBehaviour
    {
        [SerializeField] private Image theImage;
        private BuffInfo _buffInfo;
        public BuffInfo BuffInfo => _buffInfo;

        // private void Update()
        // {
        //     if (_buffInfo.durationCounter < 0)
        //     {
        //         _buffInfo = null;
        //         //todo:对象池
        //         Destroy(gameObject);
        //     }
        // }

        public void Init(BuffInfo buffInfo)
        {
            _buffInfo = buffInfo;
            theImage.sprite = buffInfo.buffData.icon;
        }
    }
}