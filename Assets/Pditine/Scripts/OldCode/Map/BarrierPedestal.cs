using MoreMountains.Feedbacks;
using Pditine.Collide;
using UnityEngine;

namespace LJH.Scripts.Map
{
    public class BarrierPedestal : ColliderBase
    {
        [SerializeField] private Barrier theBarrier;
        public Barrier TheBarrier=>theBarrier;
    }
}