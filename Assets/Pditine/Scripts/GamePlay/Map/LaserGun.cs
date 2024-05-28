using System;
using Pditine.Data;
using Pditine.GamePlay.Buff;
using Pditine.Player;
using Pditine.Player.Ass;
using UnityEngine;

namespace Pditine.Map
{
    public class LaserGun : MonoBehaviour
    {
        [SerializeField] private LineRenderer theLaser;
        [SerializeField] private Transform startPoint;
        [SerializeField] private LayerMask layerMask;
        
        [SerializeField][Range(0,1)] private float rotateSpeed;

        [SerializeField] private int atk;
        private int ATK => atk;
        private bool _ready = true;

        private Vector3 CurrentDirection => theLaser.transform.rotation*Vector3.down;
        private float _angleZTarget = 90;

        private void Start()
        {
            theLaser.SetPosition(0,startPoint.position);
        }
        private void FixedUpdate()
        {
            UpdateDirection();
            UpdateLaser();
        }

        private void UpdateLaser()
        {
            var result1 = Physics2D.Raycast(startPoint.position, CurrentDirection, 100, layerMask);
            theLaser.SetPosition(1,result1.centroid);
            var theAss = result1.collider.transform.GetComponent<AssBase>();
            if(theAss)
                HitPlayer(theAss.ThePlayer);
        }

        private void UpdateDirection()
        {
            if (Mathf.Abs(_angleZTarget-theLaser.transform.eulerAngles.z)<1)
            {
                theLaser.enabled = false;
                _ready = true;
                return;
            }

            theLaser.transform.Rotate(0, 0, rotateSpeed);

        }

        public void Trigger()
        {
            if (!_ready) return;
            _ready = false;
            theLaser.enabled = true;
            rotateSpeed = -rotateSpeed;
            if (rotateSpeed > 0) _angleZTarget = 90;
            else _angleZTarget = 270;
        }
        

        private void HitPlayer(PlayerController thePlayer)
        {
            if (thePlayer.isInvincible) return;
            thePlayer.BeHitAssFeedback();
            thePlayer.ChangeHP(-atk);
            
            BuffManager.Instance.AttachBuff(new BuffInfo(DataManager.Instance.GetBuffData(9),null,thePlayer));
            
        }
    }
}