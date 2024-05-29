using System;
using Pditine.Collide;
using UnityEngine;

namespace Pditine.Map
{
    public class LightBallBall : MonoBehaviour
    {
        [SerializeField] private Wall theBall;

        private void Start()
        {
            theBall.OnCollide += CreateLightBall;
        }

        private void CreateLightBall(ColliderBase _,CollideInfo info)
        {
            //CreateLightBall();
        }
    }
}