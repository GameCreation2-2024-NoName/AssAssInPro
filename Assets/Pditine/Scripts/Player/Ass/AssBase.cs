using System.Collections.Generic;
using Pditine.Collide;
using Pditine.Collide.CollideEvent;
using Pditine.Component;
using Pditine.Data.Ass;
using UnityEngine;
using UnityEngine.Events;

namespace Pditine.Player.Ass
{
    public abstract class AssBase : ColliderBase
    {
        protected PlayerController thePlayer;
        public PlayerController ThePlayer => thePlayer;

        private SpriteRenderer theSpriteRenderer => GetComponent<SpriteRenderer>();

        [SerializeField]protected AssDataBase data;
           
        public AssDataBase Data => data;

        public UnityAction<ColliderBase> OnBeAttack;

        public UnityAction<ColliderBase> OnBeAttackByThorn;

        [SerializeField] private TrailRenderer theTrail;

        [SerializeField] private SpriteEffect_Flash spriteEffectFlash;
        public SpriteEffect_Flash SpriteEffectFlash => spriteEffectFlash;
        
        protected override List<CollidingEventBase> GetCollidingEvents()
        {
            return new(){new Thorn_AssEvent(),new BarrierThorn_AssEvent()};
        }

        public virtual void Init(PlayerController parent)
        {
            thePlayer = parent;
            transform.position = parent.transform.position;
            transform.rotation = parent.transform.rotation;
            transform.parent = parent.transform;

            theSpriteRenderer.sprite = parent.ID == 1 ? data.PortraitBlue : data.PortraitYellow;

            theTrail.startColor = parent.ID == 1 ? new Color(0, 1, 1) : new Color(1, 0.8f, 0.063f);
            theTrail.endColor = parent.ID == 1 ? new Color(0, 1, 1,0) : new Color(1, 0.8f, 0.063f,0);
        }
    }
}