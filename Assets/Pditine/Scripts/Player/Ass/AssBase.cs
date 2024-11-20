using System.Collections.Generic;
using Pditine.Collide;
using Pditine.Collide.CollideEvent;
using Pditine.Component;
using Pditine.Data.Ass;
using Pditine.Player.VFX;
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

        [SerializeField] private SpriteEffect_Flash spriteEffectFlash;
        public SpriteEffect_Flash SpriteEffectFlash => spriteEffectFlash;

        [SerializeField] private List<string> vfxNameList = new();
        [SerializeField] private List<VFXBase> vfxList = new();
        
        protected override List<CollidingEventBase> GetCollidingEvents()
        {
            return new(){new Thorn_AssEvent(),new BarrierThorn_AssEvent()};
        }

        public virtual void Init(PlayerController parent)
        {
            thePlayer = parent;
            transform.position = parent.transform.position;
            transform.rotation = parent.transform.rotation;
            transform.parent = parent.Entity;
            theSpriteRenderer.sprite = parent.ID == 1 ? data.PortraitBlue : data.PortraitYellow;
            parent.VFX.AddVFX(vfxNameList, vfxList);
        }
    }
}