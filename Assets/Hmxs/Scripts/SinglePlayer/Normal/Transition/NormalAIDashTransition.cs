using Hmxs.Scripts.FSM;

namespace Hmxs.Scripts.SinglePlayer.Transition
{
    public class NormalAIDashTransition : ITransition<NormalAIState>
    {
        public bool Transit(out NormalAIState type)
        {
            type = NormalAIState.Idle;
            return true;
        }
    }
}