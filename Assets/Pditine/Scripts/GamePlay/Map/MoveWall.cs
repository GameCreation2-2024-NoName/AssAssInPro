using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pditine.Map
{
    public class MoveWall : MonoBehaviour
    {
        [SerializeField][Range(0,1)] private float speed;
        [SerializeField] private List<Transform> points = new();
        private int _currentIndex;
        
        private void FixedUpdate()
        {
            transform.position = Vector3.MoveTowards(transform.position, points[_currentIndex].position, speed);
            if (transform.position.Equals(points[_currentIndex].position))
                UpdateIndex();
            
        }

        private void UpdateIndex()
        {
            _currentIndex++;
            if (_currentIndex >= points.Count) _currentIndex = 0;
        }
    }
}