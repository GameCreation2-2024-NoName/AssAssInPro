// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_11_14
// -------------------------------------------------

using Pditine.Player;
using Unity.Mathematics;
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
        private bool _isCharging;
        [SerializeField]private CanvasGroup canvasGroup;
        [SerializeField] private float CD;
        private float _currentCD;
        [SerializeField] private RectTransform parentRectTransform;
        

        public void Init(PlayerController thePlayer)
        {
            thePlayer.OnChanging += Charge;
            _currentBar = thePlayer.ID == 1? blueBar: yellowBar;
            _currentBar.enabled = true;
            _target = thePlayer.transform;
        }
        
        private void FixedUpdate()
        {
            _currentCD -= Time.deltaTime;
            UpdateBar();
        }

        private void UpdateBar()
        {
            Vector2 screenPoint = Vector2.zero;
            //由于Canvas渲染模式是Screen Space - Camera,所以需要特殊处理
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform,
                UnityEngine.Camera.main.WorldToScreenPoint(_target.position), UnityEngine.Camera.main, out screenPoint); 
            transform.right = _target.right;
            transform.localPosition = screenPoint - (Vector2)(transform.up * offset);
            
            if (_currentCD < 0)
                canvasGroup.alpha = math.lerp(canvasGroup.alpha, 0, 0.1f);
            else 
                canvasGroup.alpha = 1;
        }

        private void Charge(float value)
        {
            _isCharging = true;
            _currentBar.fillAmount = value / _max;
            _currentCD = CD;
        }
    }
}