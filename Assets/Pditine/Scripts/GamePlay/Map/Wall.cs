using System.Collections.Generic;
using Pditine.Collide;
using Pditine.Collide.CollideEvent;
using UnityEngine;

namespace Pditine.Map
{
    public class Wall : ColliderBase
    {
        public override string ColliderTag => "Wall";
        //[SerializeField] private float offset;
        //[SerializeField] private Vector2 normalDirection;

        //public Vector2 NormalDirection => normalDirection;

        protected override List<CollidingEventBase> GetCollidingEvents()
        {
            return new()
            {
                new Wall_BarrierThorn(), new Wall_ThornEvent(),
                new Wall_BarrierPedestalEvent()
            };
        }
    }
}