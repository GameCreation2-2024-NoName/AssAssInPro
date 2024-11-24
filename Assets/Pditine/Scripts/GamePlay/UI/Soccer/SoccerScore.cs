// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_11_24
// -------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace Pditine.GamePlay.UI
{
    public class SoccerScore : MonoBehaviour
    {
        [SerializeField] private List<SoccerScoreLight> scoreLights;

        public void SetScore(int score)
        {
            for (int i = 0; i < scoreLights.Count; i++)
            {
                scoreLights[i].Switch(i < score);
            }
        }
    }
}