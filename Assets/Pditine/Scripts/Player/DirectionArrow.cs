using System;
using UnityEngine;

namespace Pditine.Player
{
    public class DirectionArrow : MonoBehaviour
    {
        [SerializeField] private float CD;
        [Range(0, 1)] [SerializeField] private float disappearSpeed;
        private SpriteRenderer theSpriteRenderer => GetComponent<SpriteRenderer>();
        private float _currentCD;
        [SerializeField] private Sprite blue;
        [SerializeField] private Sprite yellow;

        // private void Start()
        // {
        //     theSpriteRenderer = GetComponent<SpriteRenderer>();
        // }

        private void FixedUpdate()
        {
            _currentCD -= Time.deltaTime;
            UpdateSprite();
        }

        public void Init(int id)
        {
            theSpriteRenderer.sprite = id == 1 ? blue : yellow;
        }
        
        public void ChangeDirection(Vector3 direction)
        {
            _currentCD = CD;
            transform.right = direction;
        }

        private void UpdateSprite()
        {
            Color color = theSpriteRenderer.color;
            if (_currentCD < 0)
                theSpriteRenderer.color = Color.Lerp(color, new Color(color.r, color.g, color.b, 0), disappearSpeed);
            else theSpriteRenderer.color = new Color(color.r, color.g, color.b, 1);
        }
    }
}