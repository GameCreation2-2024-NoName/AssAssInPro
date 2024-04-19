using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs.Scripts
{
    [ExecuteInEditMode]
    public class VisualBoxSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject boxPrefab;
        [SerializeField] private Transform startPoint;
        [SerializeField] private int number;
        [SerializeField] private Vector2 direction;

        [Button]
        public void InstantiateBox()
        {
            var dir = direction.normalized;
            var position = startPoint.position;
            for (int i = 0; i < number; i++)
            {
                var box = Instantiate(boxPrefab, position, Quaternion.identity);
                box.transform.SetParent(startPoint);

                var collider = box.GetComponent<BoxCollider2D>();
                position = box.transform.position +
                           new Vector3(
                               dir.x * collider.size.x * box.transform.localScale.x,
                               dir.y * collider.size.y * box.transform.localScale.y,
                               0);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            var dir = direction.normalized;
            var collider = boxPrefab.GetComponent<BoxCollider2D>();
            var position = (number - 0.5f) * new Vector3(
                dir.x * collider.size.x * collider.transform.localScale.x,
                dir.y * collider.size.y * collider.transform.localScale.y,
                0);
            Gizmos.DrawLine(transform.position, transform.position + position);
        }
    }
}