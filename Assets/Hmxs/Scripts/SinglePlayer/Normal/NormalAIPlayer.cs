using System;
using Hmxs.Scripts.FSM;
using Pditine.Player;
using UnityEngine;

namespace Hmxs.Scripts.SinglePlayer
{
    public class NormalAIPlayer : PlayerController
    {
        [SerializeField] private float detectRadius;
        [SerializeField] private float idleTime;

        private FsmSystem<NormalAIState> _fsmSystem;

        protected override void OnInit()
        {
            _fsmSystem = FsmFactory.CreateNormalAIFsm(gameObject, detectRadius, idleTime);
            _fsmSystem.StartState(NormalAIState.Idle);
        }

        protected override void OnUpdate()
        {
            if (!canMove) return;
            if (IsPause) return;

            _fsmSystem.Update();
        }

        public override void ChangeDirection(Vector3 direction)
        {
            if (!canMove) return;
            if (IsPause) return;
            InputDirection = direction;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectRadius);
        }
    }
}