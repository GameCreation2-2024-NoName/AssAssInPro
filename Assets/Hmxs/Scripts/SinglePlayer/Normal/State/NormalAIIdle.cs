using Hmxs.Scripts.FSM;
using UnityEngine;

namespace Hmxs.Scripts.SinglePlayer.Normal.State
{
    public class NormalAIIdle : StateBase<NormalAIState>
    {
        public NormalAIIdle(NormalAIState type, ITransition<NormalAIState> transition, GameObject owner)
            : base(type, transition, owner)
        {
        }

        public override void OnEnter()
        {
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
        }
    }
}