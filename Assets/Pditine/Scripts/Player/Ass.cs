using System;
using LJH.Scripts.Collide;
using Pditine.Scripts.Player;
using UnityEngine;

namespace LJH.Scripts.Player
{
    public class Ass : ColliderBase
    {
        [SerializeField] private PlayerController thePlayer;
        
        public PlayerController ThePlayer => thePlayer;

        [HideInInspector]public float CurrentScale;
        [SerializeField] private float assMinScale;

        private void Start()
        {
            CurrentScale = transform.localScale.x;
        }

        private void FixedUpdate()
        {
            DoChangeScale();
        }

        public void ChangeScale(float delta)
        {
            CurrentScale += delta;
            if (CurrentScale < assMinScale)
                CurrentScale = assMinScale;
        }

        private void DoChangeScale()
        {
            if (transform.localScale.x.Equals(CurrentScale)) return;
            transform.localScale = Vector3.Lerp(transform.localScale,new Vector3(CurrentScale,CurrentScale,CurrentScale),0.02f);
        }
    }
}