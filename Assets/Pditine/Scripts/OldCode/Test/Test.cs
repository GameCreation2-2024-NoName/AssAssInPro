using System;
using LJH.Scripts.Player;
using Pditine.Scripts.Player;
using UnityEngine;

namespace LJH.Scripts.Utility
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        private void Update()
        {
            // if(Input.GetKeyDown(KeyCode.K))
            // {
            //     _playerController.NextThorn();
            //     _playerController.NextAss();
            // }
            // if(Input.GetKeyDown(KeyCode.J))
            // {
            //     _playerController.LastAss();
            //     _playerController.LastThorn();
            // }
        }
    }
}