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
        private PlayerController _thePlayer;
        public override void Init(PlayerController thePlayer)
        {
            _thePlayer = thePlayer;
        }

        public override void Play(object data, Action callback)
        {
            (Vector3, Vector2) posAndDir = data is (Vector3, Vector2) ? 
                ((Vector3, Vector2))data : (_thePlayer.transform.position, new Vector3(0,15,0));
            transform.position = posAndDir.Item1;
            var velocityOverLifeTime = particleSystem.velocityOverLifetime;
            velocityOverLifeTime.x = new ParticleSystem.MinMaxCurve(0, posAndDir.Item2.x);
            velocityOverLifeTime.y = new ParticleSystem.MinMaxCurve(0, posAndDir.Item2.y);
            feedback.PlayFeedbacks();
        }

        public override void Stop(object data, Action callback)
        {
            
        }
    }
}