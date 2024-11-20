// -------------------------------------------------
// Copyright@ The Most Devout Followers of McDonald
// Author : lijianhao
// Date: 2024_11_20
// -------------------------------------------------

using MoreMountains.Feedbacks;
using UnityEngine;

namespace Pditine.GamePlay.LightBall
{
    public class LightBallRef : MonoBehaviour
    {
        //[SerializeField] private MMF_Player born;
        [SerializeField] private MMF_Player eaten;
        public MMF_Player Eaten => eaten;
        [SerializeField] private MMF_Player appear;
        public MMF_Player Appear => appear;
        [SerializeField] private ParticleSystem halo;
        public ParticleSystem Halo => halo;
        [SerializeField] private ParticleSystem spark;
        public ParticleSystem Spark => spark;
        [SerializeField] private GameObject theBall;
        public GameObject TheBall => theBall;
    }
}