// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_10_15
// -------------------------------------------------

using System;
using Pditine.Player;
using PurpleFlowerCore;
using PurpleFlowerCore.Component;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class Energy : MonoBehaviour
    {
        //[SerializeField]private Image fillImage;
        [SerializeField]private PropertyBar bar1;
        [SerializeField]private PropertyBar bar2;
        [SerializeField] private Image edge;
        [SerializeField] private float edgeOffset;
        [SerializeField] private float bar2Speed = 1;
        private float _bar2Target;
        private bool _bar2IsMoving;

        public void Init(PlayerController player)
        {
            player.OnChangeEnergy += UpdateEnergy;
        }

        // private void Update()
        // {
        //     UpdateBar2();
        // }

        private void UpdateEnergy(float currentEnergy, float maxEnergy, float _)
        {
            PFCLog.Info("EnergySlider", $"currentEnergy: {currentEnergy}, maxEnergy: {maxEnergy}");
            float num = currentEnergy / maxEnergy;
            PFCLog.Info("EnergySlider", $"num: {num}");
            bar2.Value = 1-currentEnergy / maxEnergy;
            bar1.Value = 1-currentEnergy / maxEnergy;
            edge.transform.position = bar1.EdgePosition + bar1.transform.right * edgeOffset;
            // if(!_bar2IsMoving)
            //     DelayUtility.Delay(1,()=>_bar2Target = bar1.Value);
            // _bar2IsMoving = true;
        }
        
        // private void UpdateBar2()
        // {
        //     if(_bar2Target.Equals(bar2.Value))
        //     {
        //         _bar2IsMoving = false;
        //         return;
        //     }
        //     bar2.Value = Mathf.MoveTowards(bar2.Value, _bar2Target, Time.deltaTime * bar2Speed);
        // }
    }
}