using System.Collections.Generic;
using Pditine.Collide;
using Pditine.Collide.CollideEvent;
using Pditine.Component;
using Pditine.Data.Thorn;
using Pditine.Player.VFX;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Pditine.Player.Thorn
{
    public abstract class ThornBase : ColliderBase
    {
        protected PlayerController thePlayer;
        public PlayerController ThePlayer => thePlayer;

        [SerializeField]protected ThornDataBase data;
        public ThornDataBase Data => data;

        public UnityAction OnAttack;
        
        //todo:闪光加入VFX
        [SerializeField] private SpriteEffect_Flash spriteEffectFlash;
        public SpriteEffect_Flash SpriteEffectFlash => spriteEffectFlash;
        
        [SerializeField] private List<string> vfxNameList = new();
        [SerializeField] private List<VFXBase> vfxList = new();

        protected override List<CollidingEventBase> GetCollidingEvents()
        {
            return new(){new Thorn_ThornEvent(),new Wall_ThornEvent(),new Thorn_AssEvent(),new BarrierPedestal_ThornEvent(),new BarrierThorn_ThornEvent()};
        }
        
        public virtual void Init(PlayerController parent)
        {
            thePlayer = parent;
            transform.position = parent.transform.position;
            transform.rotation = parent.transform.rotation;
            transform.parent = parent.Entity;
            
            parent.VFX.AddVFX(vfxNameList, vfxList);
        }

        private void Start()
        {
            OnCollide += HandleVibration;
        }
        
        //todo:震动加入VFX
        private void HandleVibration(ColliderBase _,CollideInfo __)
        {
            if (!thePlayer.InputHandler) return;
            Gamepad theGamepad = null;
            if (thePlayer.InputHandler.IsGamepad)
                theGamepad = thePlayer.InputHandler.Devices[0] as Gamepad;
            if (theGamepad==null) return;
            theGamepad.SetMotorSpeeds(0.5f,0.5f);
            DelayUtility.Delay(0.3f,() =>
            {
                theGamepad.ResetHaptics();
            });
        }
    }
}