﻿using Pditine.Collide;
using Pditine.GamePlay.Buff;
using Pditine.Player.Thorn;
using UnityEngine;

namespace Pditine.Player.Ass
{
    public class AssAss : AssBase
    {
        [SerializeField] private BuffData assAssBuffData;
        
        public override void Init(PlayerController parent)
        {
            base.Init(parent);
            OnBeAttack += AddBuff;
        }

        private void AddBuff(ColliderBase theThorn)
        {
            var targetPlayer = (theThorn as ThornBase).ThePlayer;
            BuffManager.Instance.AttachBuff(new BuffInfo(assAssBuffData, gameObject,targetPlayer));
        }
    }
}