using System;
using Pditine.Player;
using UnityEngine;

namespace Pditine.Map
{
    public class LaserGun : MonoBehaviour
    {
        [SerializeField] private LineRenderer theLaser;
        [SerializeField] private Transform startPoint;
        [SerializeField] private LayerMask playerLayerMask;
        [SerializeField] private LayerMask wallLayerMask;
        
        [SerializeField][Range(0,1)] private float rotateSpeed;

        private bool _isDirection1 = true;

        private Vector3 currentDirection;
        private Vector3 target;

        private void Start()
        {
            // currentDirection = direction1;
            // target = direction1;
            theLaser.SetPosition(0,startPoint.position);
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))Trigger();
        }

        private void FixedUpdate()
        {
            UpdateDirection();
            UpdateLaser();

        }

        private void UpdateLaser()
        {
            var result1 = Physics2D.Raycast(startPoint.position, currentDirection, 100, playerLayerMask);
            if (result1)
            {
                theLaser.SetPosition(1,result1.centroid);
                HitPlayer(result1.transform.GetComponent<PlayerController>());
                return;
            }
            var result2 = Physics2D.Raycast(startPoint.position, currentDirection, 100, wallLayerMask);
            theLaser.SetPosition(1,result2.centroid);
        }

        private void UpdateDirection()
        {
            if (currentDirection.Equals(target))
            {
                theLaser.enabled = false;
                return;
            }
            currentDirection = Vector3.MoveTowards(currentDirection, target, rotateSpeed);
            
        }

        public void Trigger()
        {
            _isDirection1 = !_isDirection1;
            // if (_isDirection1) target = direction1;
            // else target = direction2;
            theLaser.enabled = true;
        }
        

        private void HitPlayer(PlayerController thePlayer)
        {
            
        }
    }
}