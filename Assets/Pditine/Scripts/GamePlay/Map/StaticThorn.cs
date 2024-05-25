using System.Collections.Generic;
using Pditine.Collide;
using Pditine.Collide.CollideEvent;
using UnityEngine;

namespace Pditine.Map
{
    public class StaticThorn : ColliderBase
    {
        [SerializeField] private float pushForce;
        [SerializeField] private int atk;
        public float PushForce => pushForce;
        public int ATK => atk;
        protected override List<CollidingEventBase> GetCollidingEvents()
        {
            return new(){new StaticThorn_AssEvent(),new StaticThorn_ThornEvent()};
        }
    }
}