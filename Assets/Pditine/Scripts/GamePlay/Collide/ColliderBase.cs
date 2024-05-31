using System;
using System.Collections.Generic;
using Pditine.Collide.CollideEvent;
using PurpleFlowerCore;
using PurpleFlowerCore.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace Pditine.Collide
{
    public abstract class ColliderBase : MonoBehaviour
    {
        private readonly HashSet<ColliderBase> _collidingColliders = new();
        // protected UnityAction collisionEvent;
        // public UnityAction CollisionEvent => collisionEvent;
        
        //protected static List<CollidingEventBase> _events;
        protected List<CollidingEventBase> _events;
        public List<CollidingEventBase> Events => _events;

        public UnityAction<ColliderBase,CollideInfo> OnCollide;

        protected virtual void Awake()
        {
            _events = GetCollidingEvents();
        }

        protected abstract List<CollidingEventBase> GetCollidingEvents();
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            var otherCollider = other.collider.gameObject.GetComponent<ColliderBase>();
            if (!otherCollider) return;
            if (_collidingColliders.Contains(otherCollider)) return;
            AddCollider(otherCollider);
            otherCollider.AddCollider(this);
            CollideHandler.ColliderHandle(new CollideInfo(gameObject.tag,otherCollider.gameObject.tag,this,otherCollider,other));
        }
        
        public void AddCollider(ColliderBase theCollider)
        {
            _collidingColliders.Add(theCollider);
            DelayUtility.Delay(0.1f, () =>
            {
                if (_collidingColliders.Contains(theCollider))
                    _collidingColliders.Remove(theCollider);
            });
        }
    }
}