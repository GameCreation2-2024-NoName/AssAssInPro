// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_11_14
// -------------------------------------------------

using System.Collections.Generic;
using Pditine.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Pditine.GamePlay.UI
{
    public class Mark : MonoBehaviour
    {
        [SerializeField]private List<Sprite> playerMark;
        [SerializeField]private Image image;
        
        public void Init(PlayerController playerController)
        {
            image.sprite = playerController.IsAI ? playerMark[0] : playerMark[playerController.ID];
        }
    }
}