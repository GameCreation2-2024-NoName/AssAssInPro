using System;
using System.Collections.Generic;
using Pditine.Collide;
using Pditine.Collide.CollideEvent;
using UnityEngine;

namespace Pditine.Map
{
    public class HollowCircle : ColliderBase
    {
        [SerializeField] private float speed;
        private void Update()
        {
            transform.Rotate(0,0,speed*Time.deltaTime);
        }

        protected override List<CollidingEventBase> GetCollidingEvents()
        {
            return new() { new Wall_ThornEvent(), new Wall_BarrierThorn(), new Wall_BarrierPedestalEvent() };
        }
    }
}