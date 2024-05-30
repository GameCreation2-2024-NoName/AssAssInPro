using System;
using UnityEngine;

namespace Pditine.Map
{
    public class TennisBoundary : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Tennis"))
            {
                var theTennis = other.GetComponent<Tennis>();
                theTennis.direction = -theTennis.direction;
            }
        }
    }
}