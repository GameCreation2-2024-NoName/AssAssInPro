using System;
using System.Collections.Generic;
using Pditine.Collide;
using Pditine.Collide.CollideEvent;
using UnityEngine;

namespace Pditine.Map
{
    public class Tennis : ColliderBase
    {
        public override string ColliderTag => "Tennis";
        [SerializeField] private float targetSpeed;
        [SerializeField] private float weight;
        public float Weight => weight;
        [HideInInspector]public float currentSpeed;
        
        public Vector3 direction = new(0,1,0);

        private void Start()
        {
            currentSpeed = targetSpeed;
        }

        private void FixedUpdate()
        {
            transform.position += direction * (currentSpeed * Time.deltaTime);
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 0.04f);
        }

        protected override List<CollidingEventBase> GetCollidingEvents()
        {
            return new() { new Tennis_ThornEvent(),new Tennis_AssEvent(),new Tennis_DynamicBarrierEvent(),new Tennis_WallEvent() };
        }
    }
}