using System;
using UnityEngine;

namespace Pditine.Player.Thorn
{
    public class Death : ThornBase
    {
        [SerializeField] private float rotateSpeed;
        public override string ColliderTag => "Thorn";
        private void FixedUpdate()
        {
            transform.Rotate(0,0,rotateSpeed);
        }
    }
}