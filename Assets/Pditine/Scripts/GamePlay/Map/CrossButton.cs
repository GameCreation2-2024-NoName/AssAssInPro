using System;
using System.Collections.Generic;
using Pditine.Collide;
using Pditine.Collide.CollideEvent;
using UnityEngine;

namespace Pditine.Map
{
    public class CrossButton : ColliderBase
    {
        public override string ColliderTag => "CrossButton";
        [SerializeField] private float CD = 5f;
        [SerializeField] private Cross theCross;
        [SerializeField] private Sprite readySprite;
        [SerializeField] private Sprite sleepSprite;
        [SerializeField] private Transform cdMask;
        private SpriteRenderer TheSpriteRenderer => GetComponent<SpriteRenderer>();
        
        private float _currentCD;
        private bool _ready;

        private void Update()
        {
            UpdateCD();
        }

        public void TryTrigger()
        {
            if (!_ready) return;
            theCross.Trigger();
        }

        public void ResetCD()
        {
            TheSpriteRenderer.sprite = sleepSprite;
            _currentCD = CD;
            
        }

        protected override List<CollidingEventBase> GetCollidingEvents()
        {
            return new() { new CrossButtonl_ThornEvent() };
        }

        private void UpdateCD()
        {
            _currentCD -= Time.deltaTime;
            if (_currentCD < 0)
            {
                _currentCD = 0;
                if (!_ready)
                {
                    _ready = true;
                    TheSpriteRenderer.sprite = readySprite;
                }
            }
            else
            {
                _ready = false;
                cdMask.localScale = new Vector3(1, 1-_currentCD/CD, 1);
            }
        }
    }
}