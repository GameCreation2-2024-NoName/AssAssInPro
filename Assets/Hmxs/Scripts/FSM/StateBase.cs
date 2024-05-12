using System;

namespace Hmxs.Scripts.FSM
{
    public abstract class StateBase<T> where T : Enum
    {
        protected StateBase(T type, ITransition<T> transition)
        {
            Type = type;
            _transition = transition;
        }

        private readonly ITransition<T> _transition;

        public T Type { get; }

        public abstract void OnEnter();

        public abstract void OnExit();

        public abstract void OnUpdate();

        public bool Transition(out T type) => _transition.Transition(out type);
    }
}