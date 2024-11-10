// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_10_15
// -------------------------------------------------

using Pditine.Player;
using PurpleFlowerCore;
using PurpleFlowerCore.Component;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class Energy : MonoBehaviour
    {
        //[SerializeField]private Image fillImage;
        [SerializeField]private PropertyBar EnergyBar;
        [SerializeField]private PropertyBar LightBar;
        [SerializeField] private Image edge;
        [SerializeField] private float edgeOffset;

        public void Init(PlayerController player)
        {
            player.OnChangeEnergy += UpdateEnergy;
        }

        private void UpdateEnergy(float currentEnergy, float maxEnergy)
        {
            PFCLog.Info("EnergySlider", $"currentEnergy: {currentEnergy}, maxEnergy: {maxEnergy}");
            float num = currentEnergy / maxEnergy;
            PFCLog.Info("EnergySlider", $"num: {num}");
            LightBar.Value = 1-currentEnergy / maxEnergy;
            EnergyBar.Value = 1-currentEnergy / maxEnergy;
            edge.transform.position = EnergyBar.EdgePosition + EnergyBar.transform.right * edgeOffset;
        }
    }
}