using System;
using Pditine.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class Energy : MonoBehaviour
    {
        private float _energy;
        private PlayerController _player;
        [SerializeField]private Image energyBar;
        public void Init(PlayerController player)
        {
            _energy = player.Energy;
            _player = player;
        }

        private void Update()
        {
            energyBar.fillAmount = _player.CurrentEnergy / _energy;
        }
    }
}