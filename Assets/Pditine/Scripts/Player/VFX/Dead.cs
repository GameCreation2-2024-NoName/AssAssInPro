﻿// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_10_28
// -------------------------------------------------

using MoreMountains.Feedbacks;
using UnityEngine;

namespace Pditine.Player.VFX
{
    public class Dead : VFXBase
    {
        private MMF_Player _mmfPlayer;
        [SerializeField] private MMF_Player bluePlayer;
        [SerializeField] private MMF_Player yellowPlayer;
        public override void Init(PlayerController thePlayer)
        {
            _mmfPlayer = thePlayer.ID == 1 ? bluePlayer : yellowPlayer;
        }

        public override void Play(object data = null, System.Action callback = null)
        {
            _mmfPlayer.PlayFeedbacks();
        }

        public override void Stop(object data = null, System.Action callback = null)
        {
            
        }
    }
}