using Hmxs.Scripts.FSM;
using UnityEngine;

namespace Hmxs.Scripts.SinglePlayer.Transition
{
    public class NormalAIIdleTransition : ITransition<NormalAIState>
    {
        private readonly float _time;
        private float _counter;

        public NormalAIIdleTransition(float idleTime)
        {
            _time = idleTime;
            _counter = 0;
        }

        public bool Transit(out NormalAIState type)
        {
            if (_counter > _time)
            {
                _counter = 0;
                type = NormalAIState.Dash;
                return true;
            }
            type = NormalAIState.Idle;
            _counter += Time.deltaTime;
            return false;
        }
    }
}