using Hmxs.Scripts.SinglePlayer;
using Hmxs.Scripts.SinglePlayer.Normal.State;
using Hmxs.Scripts.SinglePlayer.Transition;

namespace Hmxs.Scripts.FSM
{
    public class FsmFactory
    {
        public static FsmSystem<NormalAIState> CreateNormalAIFsm()
        {
            var fsm = new FsmSystem<NormalAIState>();

            var idleTransition = new NormalAIIdleTransition();
            var dashTransition = new NormalAIDashTransition();

            var idleState = new NormalAIIdle(NormalAIState.Idle, idleTransition);
            var dashState = new NormalAIDash(NormalAIState.Dash, dashTransition);

            fsm.AddState(idleState);
            //fsm.AddState(dashState);

            fsm.StartState(NormalAIState.Idle);
            return fsm;
        }
    }
}