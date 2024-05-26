using System;
using System.Collections.Generic;
using Pditine.Collide;
using Pditine.Collide.CollideEvent;
using UnityEngine;

namespace Pditine.Map
{
    public class DestructibleBox : ColliderBase
    {
        [SerializeField]private int hp;
        private int _currentHP;
        [SerializeField] private List<Sprite> sprites;
        private SpriteRenderer TheSpriteRenderer => GetComponent<SpriteRenderer>();

        private void Start()
        {
            _currentHP = 3;
        }

        public void BeAttack()
        {
            _currentHP--;
            if(_currentHP<0)
            {
                BeDestroy();
                return;
            }

            TheSpriteRenderer.sprite = sprites[_currentHP];
        }

        private void BeDestroy()
        {
            Destroy(gameObject);
        }

        protected override List<CollidingEventBase> GetCollidingEvents()
        {
            return new() { new DestructibleBox_ThornEvent() };
        }
    }
}