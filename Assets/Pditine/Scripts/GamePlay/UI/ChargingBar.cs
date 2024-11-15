// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_11_14
// -------------------------------------------------

using Pditine.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class ChargingBar : MonoBehaviour
    {
        [SerializeField]private Image blueBar;
        [SerializeField]private Image yellowBar;
        private Image _currentBar;
        private float _max = 25; // 先写死,跟PlayerController中相同
        private Transform _target;
        [SerializeField]private float offset;

        public void Init(PlayerController thePlayer)
        {
            thePlayer.OnEndChanging += EndCharging;
            thePlayer.OnChanging += Charge;
            _currentBar = thePlayer.ID == 1? blueBar: yellowBar;
            _target = thePlayer.transform;
        }

        private void Charge(float value)
        {
            _currentBar.fillAmount = value / _max;
            var targetPosition = UnityEngine.Camera.main.WorldToScreenPoint(_target.position);
            transform.position = targetPosition + new Vector3(0, offset, 0);
            transform.right = _target.right;
        }
        
        private void EndCharging()
        {
            
        }
         
    }
}