// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_10_23
// -------------------------------------------------

using System;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Pditine.Player.VFX
{
    public class HitFeedback : VFXBase
    {
        [SerializeField] private MMF_Player feedback;
        [SerializeField] private ParticleSystem particleSystem;
        public override void Init(PlayerController thePlayer)
        {
            
        }

        public override void Play(object data, Action callback)
        {
            feedback.PlayFeedbacks();
        }

        public override void Stop(object data, Action callback)
        {
            
        }
    }
}