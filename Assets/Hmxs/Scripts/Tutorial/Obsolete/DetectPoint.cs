using System;
using UnityEngine;

namespace Hmxs.Scripts.Tutorial
{
    [Obsolete]
    public class DetectPoint : MonoBehaviour
    {
        [SerializeField] private float detectRadius = 0.5f;

        public bool HasObjectAround()
        {
            return Physics2D.OverlapCircle(transform.position, detectRadius, LayerMask.GetMask("Player")) != null;
        }
    }
}