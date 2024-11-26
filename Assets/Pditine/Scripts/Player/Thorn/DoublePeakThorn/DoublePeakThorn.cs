using Pditine.GamePlay.Buff;
using PurpleFlowerCore;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pditine.Player.Thorn.DoublePeakThorn
{
    public class DoublePeakThorn : ThornBase
    {
        [ReadOnly]private PlayerController _leftPeakTarget;
        [ReadOnly]private PlayerController _rightPeakTarget;

        [SerializeField] private BuffData doublePeakBuffData;
        public override string ColliderTag => "Thorn";
        private BuffInfo _doublePeakBuffInfo;
        private bool _canTriggerSkill;

        public override void Init(PlayerController parent)
        {
            base.Init(parent);
            _doublePeakBuffInfo = new BuffInfo(doublePeakBuffData, gameObject, thePlayer);
        }

        public void ChangeLeftPeakTarget(PlayerController target)
        {
            _leftPeakTarget = target;
            if (_leftPeakTarget == _rightPeakTarget)
                AddBuff();
            else
                RemoveBuff();
        }
        
        public void ChangeRightPeakTarget(PlayerController target)
        {
            _rightPeakTarget = target;
            if (_leftPeakTarget == _rightPeakTarget)
                AddBuff();
            else
                RemoveBuff();
        }


        private void AddBuff()
        {
            if (_canTriggerSkill) return; 
            _canTriggerSkill = true;
            PFCLog.Info("可以触发");
            BuffManager.Instance.AttachBuff(_doublePeakBuffInfo);
        }

        private void RemoveBuff()
        {
            if (!_canTriggerSkill) return; 
            _canTriggerSkill = false;
            PFCLog.Info("不可以触发");
            BuffManager.Instance.LostBuff(_doublePeakBuffInfo);
        }
    }
}