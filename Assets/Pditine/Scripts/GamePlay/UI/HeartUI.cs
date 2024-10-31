using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    [Obsolete("不再使用单个心UI")]
    public class HeartUI : MonoBehaviour
    {
        [SerializeField] private Image heartImage;
        //[SerializeField] private Image rightHalfHeart;
        [SerializeField] private Sprite leftHalfSprite;
        //[SerializeField] private Sprite rightHalfSprite;
        [SerializeField] private Sprite fullSprite;
        [SerializeField] private Sprite nullSprite;
        
        /// <param name="hp">0~2</param>
        public void SetHp(int hp)
        {
            switch (hp)
            {
                case 0:
                    heartImage.sprite = nullSprite;
                    break;
                case 1:
                    heartImage.sprite = leftHalfSprite;
                    break;
                case 2:
                    heartImage.sprite = fullSprite;
                    break;
            }
        }
    }
}