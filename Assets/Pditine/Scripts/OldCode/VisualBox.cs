using System.Collections.Generic;
using Hmxs.Toolkit.Flow.Timer;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Hmxs.Scripts
{
    [RequireComponent(typeof(Collider2D))]
    public class VisualBox : MonoBehaviour
    {
        [SerializeField] private float rayLength;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private List<Vector2> rayDirections = new()
        {
            new Vector2(-1, 0),
            new Vector2(0, 1),
            new Vector2(1, 0),
            new Vector2(0, -1)
        };
        [SerializeField] private List<VisualBox> boxesAround = new();

        [SerializeField] private MMScaleShaker shaker;
        [SerializeField] private float defaultShakeRange;
        [SerializeField] private int defaultDisperseRange;
        [SerializeField] private float defaultDisperseRate;

        private RaycastHit2D[] _hits;

        [ReadOnly] public bool hasTriggered = false;

        private void Start()
        {
            foreach (var direction in rayDirections)
            {
                _hits = Physics2D.RaycastAll(transform.position, direction, rayLength, layerMask);
                foreach (var hit in _hits)
                {
                    if (hit.collider != null && hit.collider.gameObject != gameObject)
                    {
                        var visualBox = hit.collider.GetComponent<VisualBox>();
                        if (visualBox && !boxesAround.Contains(visualBox))
                            boxesAround.Add(visualBox);
                    }
                }
            }
        }

        // Test
        // private void OnCollisionEnter2D(Collision2D other)
        // {
        //     if (other.gameObject.CompareTag("Player"))
        //     {
        //         Act();
        //     }
        // }

        public void Act(float? shakeRange = null, int? disperseRange = null, float? disperseRate = null)
        {
            if (shakeRange == null) shakeRange = defaultShakeRange;
            if (disperseRange == null) disperseRange = defaultDisperseRange;
            if (disperseRate == null) disperseRate = defaultDisperseRate;

            Act_(shakeRange.Value, disperseRange.Value, disperseRate.Value);
            Timer.Register(0.2f, () => Reset_(disperseRange.Value), useRealTime: true);
        }

        private void Act_(float shakeRange, int count, float disperseRate)
        {
            if (hasTriggered) return;
            hasTriggered = true;

            shaker.ShakeRange = shakeRange;
            shaker.Play();

            if (count <= 0) return;

            foreach (var box in boxesAround)
                box.Act_(shakeRange * disperseRate, count - 1, disperseRate);
        }

        private void Reset_(int count)
        {
            hasTriggered = false;

            if (count <= 0) return;

            foreach (var box in boxesAround) box.Reset_(count - 1);
        }

        private void OnDrawGizmos()
        {
            if (boxesAround.Count == 0)
            {
                Gizmos.color = Color.red;
                foreach (var direction in rayDirections)
                    Gizmos.DrawLine(transform.position, (Vector2)transform.position + direction * rayLength);
                return;
            }

            Gizmos.color = Color.green;
            foreach (var box in boxesAround)
                Gizmos.DrawLine(transform.position, box.transform.position);
        }
    }
}