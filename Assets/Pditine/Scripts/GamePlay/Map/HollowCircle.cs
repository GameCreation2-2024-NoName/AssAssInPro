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
        [SerializeField] private float weight;
        public float Weight => weight;

        [SerializeField] private float calculatingSpeed;
        public float CalculatingSpeed => calculatingSpeed;
        private void Update()
        {
            // if(Input.GetMouseButtonDown(0))
            //     Debug.Log(GetTangent( Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            transform.Rotate(0,0,speed*Time.deltaTime);
        }

        public Vector3 GetTangent(Vector3 position)
        {
            Vector3 dir = position - transform.position;
            return new Vector3(-dir.y,dir.x).normalized;
        }

        protected override List<CollidingEventBase> GetCollidingEvents()
        {
            return new() { new HollowCircle_ThornEvent()};
        }
    }
}