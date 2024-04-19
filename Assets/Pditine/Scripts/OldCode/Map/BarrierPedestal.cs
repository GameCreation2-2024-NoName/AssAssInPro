using LJH.Scripts.Collide;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace LJH.Scripts.Map
{
    public class BarrierPedestal : ColliderBase
    {
        [SerializeField] private Barrier theBarrier;
        public Barrier TheBarrier=>theBarrier;
    }
}