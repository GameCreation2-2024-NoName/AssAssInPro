using System;
using Pditine.Player.VFX;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Pditine.Player
{
    public class DirectionArrow : VFXBase
    {
        [SerializeField] private float CD;
        [Range(0, 1)] [SerializeField] private float disappearSpeed;
        private SpriteRenderer theSpriteRenderer => GetComponent<SpriteRenderer>();
        private float _currentCD;
        [SerializeField] private Sprite blue;
        [SerializeField] private Sprite yellow;
        private Vector3 _initialScale;

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
            Color color = theSpriteRenderer.color;
            if (_currentCD < 0)
                theSpriteRenderer.color = Color.Lerp(color, new Color(color.r, color.g, color.b, 0), disappearSpeed);
            else theSpriteRenderer.color = new Color(color.r, color.g, color.b, 1);
        }

        public override void Init(PlayerController thePlayer)
        {
            _initialScale = transform.localScale;
            theSpriteRenderer.sprite = thePlayer.ID == 1 ? blue : yellow;
            thePlayer.OnChangeCurrentDirection += ChangeDirection;
        }

        public override void Play(object data, Action callback)
        {
            
        }

        public override void Stop(object data, Action callback)
        {
            
        }
    }
}