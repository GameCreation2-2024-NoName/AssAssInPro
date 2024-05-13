using Sirenix.OdinInspector;
using UnityEngine;

namespace Pditine.Player.Thorn
{
    public class DoublePeakThorn : ThornBase
    {
        [ReadOnly]public PlayerController leftPeakTarget;
        public PlayerController rightPeakTarget;
    }
}