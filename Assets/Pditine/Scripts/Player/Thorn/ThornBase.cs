﻿using System.Collections.Generic;
using Pditine.Collide;
using Pditine.Collide.CollideEvent;
using Pditine.Scripts.Data.Ass;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pditine.Player.Thorn
{
    public abstract class ThornBase : ColliderBase
    {
        protected PlayerController thePlayer;
        public PlayerController ThePlayer => thePlayer;

        [SerializeField]protected ThornDataBase data;
        public ThornDataBase Data => data;

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

        // public void Init(ThornDataBase theData)
        // {
        //     data = theData;
        // }

        // [HideInInspector]public float CurrentScale;
        // [SerializeField] private float thornMaxScale;

        private void Start()
        {
            CallBack += HandleVibration;
            //CurrentScale = transform.localScale.x;
        }
        //
        // private void FixedUpdate()
        // {
        //     DoChangeScale();
        // }
        //
        // private void OnDisable()
        // {
        //     if (thePlayer.TheInput&&thePlayer.TheInput.devices[0] is Gamepad theGamepad) theGamepad.ResetHaptics();
        // }
        //
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
        //
        // public void ChangeScale(float delta)
        // {
        //     CurrentScale += delta;
        //     if (CurrentScale > thornMaxScale)
        //         CurrentScale = thornMaxScale;
        // }
        //
        // private void DoChangeScale()
        // {
        //     if (transform.localScale.x.Equals(CurrentScale)) return;
        //     transform.localScale = Vector3.Lerp(transform.localScale,new Vector3(CurrentScale,CurrentScale,CurrentScale),0.02f);
        // }
    }
}