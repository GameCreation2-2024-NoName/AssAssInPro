using System;
using UnityEngine;

namespace Hmxs.Scripts.FSM
{
    [Serializable]
    public abstract class StateBase<T> where T : Enum
    {
        private ITransition<T> Transition { get; }
        public T Type { get; }
        public GameObject Owner { get; }

        protected StateBase(T type, ITransition<T> transition, GameObject owner)
        {
            Type = type;
            Transition = transition;
            Owner = owner;
        }

        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void OnUpdate();

        public bool Transit(out T type) => Transition.Transit(out type);
    }
}