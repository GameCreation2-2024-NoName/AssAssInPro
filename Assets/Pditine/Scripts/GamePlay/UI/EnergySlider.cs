// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_10_15
// -------------------------------------------------

using Pditine.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class EnergySlider : MonoBehaviour
    {
        [SerializeField]private Image fillImage;
        public void Init(PlayerController player)
        {
            player.OnChangeEnergy += UpdateEnergy;
        }
        
        private void UpdateEnergy(float currentEnergy, float maxEnergy)
        {
            fillImage.fillAmount = currentEnergy / maxEnergy;
        }
    }
}