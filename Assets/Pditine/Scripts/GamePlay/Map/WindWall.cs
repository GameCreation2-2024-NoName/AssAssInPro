using System;
using UnityEngine;

namespace Pditine.Map
{
    public class WindWall : MonoBehaviour
    {
        [SerializeField] private Transform targetPoint;
        [SerializeField][Range(0,0.01f)] private float windForce = 0.005f;
        
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("DynamicBarrier"))
            {
                var theDB = other.GetComponent<DynamicBarrier>();
                theDB.Direction = Vector3.Lerp(theDB.Direction, targetPoint.position - theDB.transform.position,
                    windForce);
            }
        }
    }
}