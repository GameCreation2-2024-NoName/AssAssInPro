// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_10_23
// -------------------------------------------------

using System;
using UnityEngine;

namespace Pditine.Player.VFX
{
    public abstract class VFXBase : MonoBehaviour
    {
        public abstract void Init(PlayerController thePlayer);
        public abstract void Play(object data, Action callback);
        public abstract void Stop(object data, Action callback);
    }
}