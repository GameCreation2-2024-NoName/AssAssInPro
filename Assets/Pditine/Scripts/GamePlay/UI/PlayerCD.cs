﻿// using System;
// using Pditine.Player;
// using UnityEngine;
// using UnityEngine.UI;
//
// namespace Pditine.GamePlay.UI
// {
//     [Obsolete("不再使用CD")]
//     public class PlayerCD : MonoBehaviour
//     {
//         [SerializeField]private Image _cdUI;
//
//         public void Init(PlayerController thePlayer)
//         {
//             thePlayer.OnChangeCD += UpdateCD;
//         }
//
//         private void UpdateCD(float fillCD)
//         {
//             _cdUI.fillAmount = fillCD;
//         }
//     }
// }