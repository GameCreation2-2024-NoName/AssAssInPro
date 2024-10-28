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
    public abstract class VFXBase : MonoBehaviour
    {
        protected bool isPlayering;
        public abstract void Init(PlayerController thePlayer);
        public abstract void Play(object data = null, Action callback = null);
        public abstract void Stop(object data = null, Action callback = null);
    }
}