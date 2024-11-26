using System.Collections.Generic;
using Pditine.Collide;
using Pditine.Collide.CollideEvent;
using UnityEngine;

namespace Pditine.Map
{
    public class BarrierPedestal : ColliderBase
    {
        
        [SerializeField] private Barrier theBarrier;
        public Barrier TheBarrier=>theBarrier;
        public override string ColliderTag => "BarrierPedestal";
        protected override List<CollidingEventBase> GetCollidingEvents()
        {
            return new List<CollidingEventBase>
            {
                new BarrierPedestal_ThornEvent(),new Wall_BarrierPedestalEvent(),new BarrierThorn_BarrierPedestalEvent()
            };
        }
    }
}