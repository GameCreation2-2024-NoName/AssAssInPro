using System;

namespace Hmxs.Scripts.FSM
{
    public interface ITransition<T> where T : Enum
    {
        bool Transit(out T type);
    }
}