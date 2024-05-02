using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class HeartUI : MonoBehaviour
    {
        [SerializeField] private Image leftHalfHeart;
        [SerializeField] private Image rightHalfHeart;
        [SerializeField] private Sprite leftHalfSprite;
        [SerializeField] private Sprite rightHalfSprite;
        [SerializeField] private Sprite nullSprite;
        
        /// <param name="hp">0~2</param>
        public void SetHp(int hp)
        {
            switch (hp)
            {
                case 0:
                    leftHalfHeart.sprite = nullSprite;
                    rightHalfHeart.sprite = nullSprite;
                    break;
                case 1:
                    leftHalfHeart.sprite = leftHalfSprite;
                    rightHalfHeart.sprite = nullSprite;
                    break;
                case 2:
                    leftHalfHeart.sprite = leftHalfSprite;
                    rightHalfHeart.sprite = rightHalfSprite;
                    break;
            }
        }
    }
}