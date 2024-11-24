// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_11_14
// -------------------------------------------------

using UnityEngine;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]private Text timerText;
        [SerializeField]private Image timerImage;
        [SerializeField]private float time;
        [SerializeField] private bool unlimitedTime;

        public void Init(bool unlimitedTime)
        {
            this.unlimitedTime = unlimitedTime;
            if (unlimitedTime)
            {
                timerImage.enabled = true;
            }
            else
            {
                // timerText.text = time.ToString();
                // timerImage.fillAmount = 1;
                throw new System.NotImplementedException();
            }
        }
    }
}