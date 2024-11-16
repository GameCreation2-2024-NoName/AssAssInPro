// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_10_15
// -------------------------------------------------

using Pditine.Player;
using PurpleFlowerCore;
using PurpleFlowerCore.Component;
using PurpleFlowerCore.Utility;
using Unity.Mathematics;
using UnityEngine;
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
        [SerializeField] private float bar2CD;
        [SerializeField] private float bar2Threshold;
        private float _currentBar2CD;
        
        private float _bar2Target;
        // private bool _bar2IsMoving;

        public void Init(PlayerController player)
        {
            player.OnChangeEnergy += UpdateEnergy;
        }
        
        private void Update()
        {
            _currentBar2CD -= Time.deltaTime;
            if (_currentBar2CD < 0)
            {
                _bar2Target = bar1.Value;
                _currentBar2CD = bar2CD;
            }

            if (math.abs(_bar2Target - bar2.Value) > bar2Threshold)
            {
                DelayUtility.Delay(1,()=>_bar2Target = bar1.Value);
                _currentBar2CD = bar2CD;
            }
            
            if(_bar2Target > bar1.Value)
                bar2.Value = bar1.Value;
            UpdateBar2();
        }
        
        private void UpdateEnergy(float currentEnergy, float maxEnergy, float _)
        {
            PFCLog.Info("EnergySlider", $"currentEnergy: {currentEnergy}, maxEnergy: {maxEnergy}");
            float num = currentEnergy / maxEnergy;
            PFCLog.Info("EnergySlider", $"num: {num}");

            bar1.Value = 1-currentEnergy / maxEnergy;
            edge.transform.position = bar1.EdgePosition + bar1.transform.right * edgeOffset;
            // if(!_bar2IsMoving)
            //     DelayUtility.Delay(1,()=>_bar2Target = bar1.Value);
            // _bar2IsMoving = true;
        }
        
        private void UpdateBar2()
        {
            if(_bar2Target.Equals(bar2.Value))return;
            bar2.Value = Mathf.MoveTowards(bar2.Value, _bar2Target, UnityEngine.Time.deltaTime * bar2Speed);
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