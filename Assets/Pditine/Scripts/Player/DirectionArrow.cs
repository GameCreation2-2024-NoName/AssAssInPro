using System;
using UnityEngine;

namespace Pditine.Player
{
    public class DirectionArrow : MonoBehaviour
    {
        [SerializeField] private float CD;
        [Range(0, 1)] [SerializeField] private float disappearSpeed;
        private SpriteRenderer _theSpriteRenderer;
        private float _currentCD;

        private void Start()
        {
            _theSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            _currentCD -= Time.deltaTime;
            UpdateSprite();
        }
        
        public void ChangeDirection(Vector3 direction)
        {
            _currentCD = CD;
            transform.right = direction;
        }

        private void UpdateSprite()
        {
            Color color = _theSpriteRenderer.color;
            if (_currentCD < 0)
                _theSpriteRenderer.color = Color.Lerp(color, new Color(color.r, color.g, color.b, 0), disappearSpeed);
            else _theSpriteRenderer.color = new Color(color.r, color.g, color.b, 1);
        }
    }
}