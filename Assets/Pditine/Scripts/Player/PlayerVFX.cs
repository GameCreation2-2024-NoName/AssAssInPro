// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_10_23
// -------------------------------------------------

using System;
using System.Collections.Generic;
using Pditine.Player.VFX;
using PurpleFlowerCore;
using UnityEngine;

namespace Pditine.Player
{
    public class PlayerVFX : MonoBehaviour
    {
        //todo: 数据结构封装
        [SerializeField]private List<string> vfxNameList = new();
        [SerializeField]private List<VFXBase> vfxList = new();
        private PlayerController _thePlayer;
        public PlayerController ThePlayer => _thePlayer;
        
        public VFXBase this[string vfxName]
        {
            get
            {
                if (vfxNameList.Contains(vfxName))
                {
                    return vfxList[vfxNameList.IndexOf(vfxName)];
                }
                PFCLog.Warning("VFX", $"VFX {vfxName} not found");
                return null;
            }
        }
        
        public VFXBase this[VFXName vfxName]
        {
            get
            {
                if (vfxNameList.Contains(vfxName.ToString()))
                {
                    return vfxList[vfxNameList.IndexOf(vfxName.ToString())];
                }
                Debug.LogWarning("[VFX]" +  $"VFX {vfxName} not found");
                return null;
            }
        }

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
        
        public void AddVFX(string vfxName, VFXBase vfx)
        {
            vfxNameList.Add(vfxName);
            vfxList.Add(vfx);
            vfx.Init(_thePlayer);
        }
        
        public void AddVFX(List<string> vfxName, List<VFXBase> vfx)
        {
            for (int i = 0; i < vfxName.Count; i++)
            {
                AddVFX(vfxName[i], vfx[i]);
            }
        }
    }

    public enum VFXName
    {
        DirectionArrow,
        Hit,
        Dead,
        AssHit,
        Charging,
        ChargeDone,
        Trail,
    }
}