// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_10_31
// -------------------------------------------------

using System;
using UnityEngine;

namespace Pditine.Player.VFX
{
    public class Trail : VFXBase
    {
        [SerializeField] private TrailRenderer yellowTrial;
        [SerializeField] private TrailRenderer blueTrial;
        private TrailRenderer _theTrial;
        public TrailRenderer TheTrial => _theTrial;
        
        public override void Init(PlayerController thePlayer)
        {
            var playerIsBlue = thePlayer.ID == 1;
            yellowTrial.enabled = !playerIsBlue;
            blueTrial.enabled = playerIsBlue;
            _theTrial = playerIsBlue ? blueTrial : yellowTrial;
        }

        public override void Play(object data = null, Action callback = null)
        {
            _theTrial.enabled = true;
        }

        public override void Stop(object data = null, Action callback = null)
        {
            _theTrial.enabled = false;
        }
    }
}