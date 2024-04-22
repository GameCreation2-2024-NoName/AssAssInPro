using System;
using LJH.Scripts.Collide;
using Pditine.Scripts.Player;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LJH.Scripts.Player
{
    public class Thorn : ColliderBase
    {
        [SerializeField] private PlayerController thePlayer;
        public PlayerController ThePlayer => thePlayer;
        
        [HideInInspector]public float CurrentScale;
        [SerializeField] private float thornMaxScale;

        private void Start()
        {
            collisionEvent += HandleVibration;
            CurrentScale = transform.localScale.x;
        }

        private void FixedUpdate()
        {
            DoChangeScale();
        }

        private void OnDisable()
        {
            if (thePlayer.TheInput&&thePlayer.TheInput.devices[0] is Gamepad theGamepad) theGamepad.ResetHaptics();
        }

        private void HandleVibration()
        {
            Gamepad theGamepad = null;
            if(thePlayer.TheInput)
                theGamepad = thePlayer.TheInput.devices[0] as Gamepad;
            if (theGamepad==null) return;
            theGamepad.SetMotorSpeeds(0.5f,0.5f);
            DelayUtility.Delay(0.3f,() =>
            {
                theGamepad.ResetHaptics();
            });
        }

        public void ChangeScale(float delta)
        {
            CurrentScale += delta;
            if (CurrentScale > thornMaxScale)
                CurrentScale = thornMaxScale;
        }

        private void DoChangeScale()
        {
            if (transform.localScale.x.Equals(CurrentScale)) return;
            transform.localScale = Vector3.Lerp(transform.localScale,new Vector3(CurrentScale,CurrentScale,CurrentScale),0.02f);
        }
    }
    

}