using System;
using Pditine.Player;
using UnityEngine;

namespace Pditine.GamePlay.Buff
{
    [Serializable]
    public class BuffInfo : IComparable<BuffInfo>
    {
        public BuffData buffData;
        public GameObject creator;
        public PlayerController target;
        public float durationCounter;
        public float tickCounter;
        public int currentStack;

        public BuffInfo(BuffData buffData, GameObject creator, PlayerController target)
        {
            this.buffData = buffData;
            this.creator = creator;
            this.target = target;
        }

        public int CompareTo(BuffInfo other)
        {
            if (ReferenceEquals(this, other)) return 0;
            
            if (other == null) return 1;
            if (buffData == null) return other.buffData == null ? 0 : -1;
            if (other.buffData == null) return 1;

            if (target == other.target)
                return buffData.id.CompareTo(other.buffData.id);
            else return target.ID > other.target.ID ? 1 : -1;
        }

        public void OnInit()
        {
            if (buffData.onInitEvents is null) return;
            foreach (var onInitEvent in buffData.onInitEvents)
            {
                onInitEvent.Trigger(this);
            }
        }
        
        public void OnAttach()
        {
            if (buffData.onAttachEvents is null) return;
            foreach (var onAttachEvent in buffData.onAttachEvents)
            {
                onAttachEvent.Trigger(this);
            }
        }
        
        public void OnLost()
        {
            if (buffData.onLostEvents is null) return;
            foreach (var onLostEvent in buffData.onLostEvents)
            {
                onLostEvent.Trigger(this);
            }
        }
        
        public void OnTick()
        {
            if (buffData.onTickEvents is null) return;
            foreach (var onTickEvent in buffData.onTickEvents)
            {
                onTickEvent.Trigger(this);
            }
        }

        public void OnReset()
        {
            if (buffData.onResetEvents is null) return;
            foreach (var onResetEvent in buffData.onResetEvents)
            {
                onResetEvent.Trigger(this);
            }
        }
    }
}