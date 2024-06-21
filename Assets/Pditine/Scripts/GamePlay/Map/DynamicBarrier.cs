using System.Collections.Generic;
using Pditine.Collide;
using Pditine.Collide.CollideEvent;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Pditine.Map
{
    public class DynamicBarrier : ColliderBase
    {
        [HideInInspector]public float CurrentSpeed;
        [SerializeField] private float weight;
        [SerializeField] private float friction;
        public float Weight => weight;
        [ReadOnly]public Vector2 Direction;

        private void Start()
        {
            Direction = transform.right;
        }

        private void Update()
        {
            transform.position += (Vector3)Direction*(CurrentSpeed*Time.deltaTime);
            UpdateSpeed();
        }

        private void UpdateSpeed()
        {
            CurrentSpeed -= friction * Time.deltaTime;
            if (CurrentSpeed < 0) CurrentSpeed = 0;
        }
        
        protected override List<CollidingEventBase> GetCollidingEvents()
        {
            return new()
                { new DynamicBarrier_AssEvent(), new DynamicBarrier_ThornEvent(), new Wall_DynamicBarrierEvent() };
        }
    }
}