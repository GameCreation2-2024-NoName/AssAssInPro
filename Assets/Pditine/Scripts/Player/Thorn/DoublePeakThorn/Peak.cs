using System;
using Pditine.Player.Ass;
using UnityEngine;

namespace Pditine.Player.Thorn.DoublePeakThorn
{
    public class Peak : MonoBehaviour
    {
        [SerializeField] private DoublePeakThorn parent;
        [SerializeField] private bool isLeft;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Ass")&& isLeft)
                parent.ChangeLeftPeakTarget(other.GetComponent<AssBase>().ThePlayer);
            if(other.CompareTag("Ass")&& !isLeft)
                parent.ChangeRightPeakTarget(other.GetComponent<AssBase>().ThePlayer);
            if(other.CompareTag("Thorn")&& isLeft)
                parent.ChangeLeftPeakTarget(other.GetComponent<ThornBase>().ThePlayer);
            if(other.CompareTag("Thorn")&& !isLeft)
                parent.ChangeRightPeakTarget(other.GetComponent<ThornBase>().ThePlayer);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if((other.CompareTag("Ass")|| other.CompareTag("Thorn"))&& isLeft)
                parent.ChangeLeftPeakTarget(null);
            if((other.CompareTag("Ass")|| other.CompareTag("Thorn"))&& !isLeft)
                parent.ChangeRightPeakTarget(null);

        }
    }
}