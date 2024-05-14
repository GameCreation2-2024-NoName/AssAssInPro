using Hmxs.Scripts.SinglePlayer;
using Hmxs.Scripts.SinglePlayer.Normal.State;
using Hmxs.Scripts.SinglePlayer.Transition;
using UnityEngine;

namespace Hmxs.Scripts.FSM
{
    public static class FsmFactory
    {
        public static FsmSystem<NormalAIState> CreateNormalAIFsm(GameObject owner, float detectRadius, float idleTime)
        {
            var fsm = new FsmSystem<NormalAIState>();

            var idleTransition = new NormalAIIdleTransition(idleTime);
            var dashTransition = new NormalAIDashTransition();

            var idleState = new NormalAIIdle(NormalAIState.Idle, idleTransition, owner);
            var dashState = new NormalAIDash(NormalAIState.Dash, dashTransition, owner, detectRadius);

            fsm.AddState(idleState);
            fsm.AddState(dashState);

            return fsm;
        }
    }
}