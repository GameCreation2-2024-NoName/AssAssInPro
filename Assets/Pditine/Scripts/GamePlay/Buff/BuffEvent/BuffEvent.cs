using UnityEngine;

namespace Pditine.GamePlay.Buff
{
    public abstract class BuffEvent : ScriptableObject
    {
        public abstract void Trigger(BuffInfo buffInfo);
    }
}