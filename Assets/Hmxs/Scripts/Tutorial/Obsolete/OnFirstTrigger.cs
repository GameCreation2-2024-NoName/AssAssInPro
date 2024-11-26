using System;
using System.Collections.Generic;
using System.Linq;
using Pditine.Collide;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Hmxs.Scripts.Tutorial
{
    [Obsolete]
    public class OnFirstTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent onFirstCollide;
        [SerializeField] private List<string> tagsList;
        [SerializeField] [ReadOnly] private bool isCollided;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var otherCollider = other.gameObject.GetComponent<ColliderBase>();
            if (!otherCollider) return;
            foreach (var _ in tagsList.Where(t => !isCollided && otherCollider.gameObject.CompareTag(t)))
            {
                onFirstCollide?.Invoke();
                isCollided = true;
                Destroy(GetComponent<OnFirstTrigger>());
            }
        }
    }
}