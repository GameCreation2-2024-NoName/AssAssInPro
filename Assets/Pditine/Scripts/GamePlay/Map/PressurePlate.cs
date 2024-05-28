using System;
using UnityEngine;

namespace Pditine.Map
{
    public class PressurePlate : MonoBehaviour
    {
        [SerializeField] private LaserGun theLaserGun;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Thorn") || other.CompareTag("Ass"))
            {
                theLaserGun.Trigger();
            }
        }
    }
}