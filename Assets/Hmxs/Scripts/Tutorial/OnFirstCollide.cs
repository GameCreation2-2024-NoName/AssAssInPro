using System.Collections.Generic;
using System.Linq;
using Pditine.Collide;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Hmxs.Scripts.Tutorial
{
    public class OnFirstCollide : MonoBehaviour
    {
        [SerializeField] private UnityEvent onFirstCollide;
        [SerializeField] private List<string> tagsList;
        [SerializeField] [ReadOnly] private bool isCollided;
        private void OnCollisionEnter2D(Collision2D other)
        {
            var otherCollider = other.collider.gameObject.GetComponent<ColliderBase>();
            if (!otherCollider) return;
            foreach (var _ in tagsList.Where(t => !isCollided && otherCollider.gameObject.CompareTag(t)))
            {
                onFirstCollide?.Invoke();
                isCollided = true;
                Destroy(GetComponent<OnFirstCollide>());
            }
        }
    }
}