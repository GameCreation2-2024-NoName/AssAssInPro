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
        protected override List<CollidingEventBase> GetCollidingEvents()
        {
            return new(){new BarrierPedestal_ThornEvent(),new Boundary_BarrierPedestalEvent(),new BarrierThorn_BarrierPedestalEvent()};
        }
    }
}