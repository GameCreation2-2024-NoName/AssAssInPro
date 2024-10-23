// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_10_23
// -------------------------------------------------

using System;
using System.Collections.Generic;
using Pditine.Player.VFX;
using UnityEngine;

namespace Pditine.Player
{
    public class PlayerVFX : MonoBehaviour
    {
        [SerializeField]private List<string> vfxNameList = new();
        [SerializeField]private List<VFXBase> vfxList = new();
        private PlayerController _thePlayer;
        public PlayerController ThePlayer => _thePlayer;

        public void Init(PlayerController thePlayer)
        {
            _thePlayer = thePlayer;
            foreach (var vfx in vfxList)
            {
                vfx.Init(_thePlayer);
            }
        }
        
        public void Play(string vfxName, object data = null, Action callback = null)
        {
            if (vfxNameList.Contains(vfxName))
            {
                vfxList[vfxNameList.IndexOf(vfxName)].Play(data, callback);
            }
        }
        
        public void Stop(string vfxName, object data = null, Action callback = null)
        {
            if (vfxNameList.Contains(vfxName))
            {
                vfxList[vfxNameList.IndexOf(vfxName)].Stop(data, callback);
            }
        }
    }
}