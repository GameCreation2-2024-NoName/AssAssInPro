using System.Collections.Generic;
using Pditine.Collide;
using Pditine.Collide.CollideEvent;
using UnityEngine;

namespace Pditine.Map
{
    public class BarrierThorn : ColliderBase
    {
        [SerializeField] private Barrier theBarrier;
        public Barrier TheBarrier=>theBarrier;

        protected override List<CollidingEventBase> GetCollidingEvents()
        {
            return new List<CollidingEventBase>
            {
                new Wall_BarrierThorn(), new BarrierThorn_BarrierPedestalEvent(), new BarrierThorn_AssEvent(),
                new BarrierThorn_ThornEvent(), new BarrierThorn_BarrierThornEvent()
            };
        }
    }
}