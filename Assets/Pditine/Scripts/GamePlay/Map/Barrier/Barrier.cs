using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pditine.Map
{
    public class Barrier : MonoBehaviour
    {
        [HideInInspector]public float CurrentSpeed;
        [SerializeField] private int atk;
        public int ATK => atk;
        [SerializeField] private float weight;
        public float Weight => weight;
        [SerializeField] private float rotateSpeed;
        [ReadOnly]public Vector2 Direction;
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
            CurrentSpeed -= weight*Time.deltaTime;
            if (CurrentSpeed <= 0) CurrentSpeed = 0;
        }
    }
}