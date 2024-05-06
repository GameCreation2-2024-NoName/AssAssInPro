using System;
using Pditine.GamePlay.Buff;
using Pditine.Player;
using UnityEngine;

namespace Pditine.GamePlay.LightBall
{
    public abstract class LightBallBase : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                BuffManager.Instance.AttachBuff(AddBuff(other.GetComponent<PlayerController>()));
            }
        }

        protected abstract BuffInfo AddBuff(PlayerController targetPlayer);
    }
}