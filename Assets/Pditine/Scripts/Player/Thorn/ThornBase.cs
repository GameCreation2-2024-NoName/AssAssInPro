using System.Collections.Generic;
using Pditine.Collide;
using Pditine.Collide.CollideEvent;
using Pditine.Scripts.Data.Ass;
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

        protected override List<CollidingEventBase> GetCollidingEvents()
        {
            return new(){new Thorn_ThornEvent(),new Boundary_ThornEvent(),new Thorn_AssEvent(),new BarrierPedestal_ThornEvent(),new BarrierThorn_ThornEvent()};
        }
        
        public void Init(PlayerController parent)
        {
            thePlayer = parent;
            transform.position = parent.transform.position;
            transform.rotation = parent.transform.rotation;
            transform.parent = parent.transform;
        }

        private void Start()
        {
            CallBack += HandleVibration;
        }
        private void HandleVibration()
        {
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