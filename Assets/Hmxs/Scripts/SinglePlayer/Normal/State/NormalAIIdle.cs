using Hmxs.Scripts.FSM;
using UnityEngine;

namespace Hmxs.Scripts.SinglePlayer.Normal.State
{
    public class NormalAIIdle : StateBase<NormalAIState>
    {
        public NormalAIIdle(NormalAIState type, ITransition<NormalAIState> transition) : base(type, transition)
        {
        }

        public override void OnEnter()
        {
            Debug.Log("Idle State Enter");
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate()
        {

        }
    }
}