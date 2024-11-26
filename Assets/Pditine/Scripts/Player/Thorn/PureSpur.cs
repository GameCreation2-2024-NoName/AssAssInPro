using System;
using Pditine.GamePlay.Buff;
using UnityEngine;

namespace Pditine.Player.Thorn
{
    public class PureSpur : ThornBase
    {
        [SerializeField] private BuffData pureSpurBuffData;
        private BuffInfo _pureSpurBuffInfo;
        public override string ColliderTag => "Thorn";

        public override void Init(PlayerController parent)
        {
            base.Init(parent);
            _pureSpurBuffInfo = new BuffInfo(pureSpurBuffData, gameObject, thePlayer);
        }

        
    }
}