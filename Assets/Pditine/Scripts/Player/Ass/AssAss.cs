using Pditine.Collide;
using Pditine.GamePlay.Buff;
using Pditine.Map;
using Pditine.Player.Thorn;
using UnityEngine;

namespace Pditine.Player.Ass
{
    public class AssAss : AssBase
    {
        [SerializeField] private BuffData assAssBuffData;
        public override string ColliderTag => "Ass";
        
        public override void Init(PlayerController parent)
        {
            base.Init(parent);
            //OnBeAttack += AddBuff;
        }

        private void AddBuff(ColliderBase theThorn)
        {
            if (theThorn is not ThornBase @base) return;
            var targetPlayer = @base.ThePlayer;
            BuffManager.Instance.AttachBuff(new BuffInfo(assAssBuffData, gameObject,targetPlayer));
        }

        
    }
}