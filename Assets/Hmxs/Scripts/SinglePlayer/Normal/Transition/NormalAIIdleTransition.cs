using Hmxs.Scripts.FSM;

namespace Hmxs.Scripts.SinglePlayer.Transition
{
    public class NormalAIIdleTransition : ITransition<NormalAIState>
    {
        public bool Transition(out NormalAIState type)
        {
            type = NormalAIState.Dash;
            return true;
        }
    }
}