using MoreMountains.Feedbacks;
using UnityEngine;

namespace Pditine.Map
{
    public class Barrier : MonoBehaviour
    {
        [HideInInspector]public float CurrentSpeed;
        [SerializeField] private float friction;
        [SerializeField] private float rotateSpeed;
        [HideInInspector]public Vector2 Direction;
        public MMF_Player collideWithBoundary;
        [SerializeField] private BarrierThorn thorn;
        public BarrierThorn TheThorn => thorn;
        [SerializeField] private BarrierPedestal pedestal;
        public BarrierPedestal ThePedestal=>pedestal;
        [SerializeField] private MMF_Player hit;
        public MMF_Player HitFeedback => hit;

        private void Start()
        {
            Direction = transform.right;
        }

        private void FixedUpdate()
        {
            transform.position += (Vector3)Direction*(CurrentSpeed*Time.deltaTime);
            if(CurrentSpeed>0)
                transform.right = Vector3.Lerp(transform.right, Direction, rotateSpeed);
        }

        private void Update()
        {
            ReduceSpeed();
        }

        private void ReduceSpeed()
        {
            CurrentSpeed -= friction*Time.deltaTime;
            if (CurrentSpeed <= 0) CurrentSpeed = 0;
        }
    }
}