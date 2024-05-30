﻿using System.Collections.Generic;
using Pditine.GamePlay.LightBall;
using UnityEngine;


namespace Pditine.Map
{
    public class BallBoundary : MonoBehaviour
    {
        [SerializeField] private Transform createPoint;
        [SerializeField] private List<GameObject> lightBalls = new();
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("DynamicBarrier"))
                CreateLightBall();

        }
        
        private void CreateLightBall()
        {
            //todo:对象池
            //var theBall = PoolSystem.GetGameObject(lightBalls[Random.Range(0, lightBalls.Count)]).GetComponent<LightBall>();

            var overlaps = Physics2D.OverlapPointAll(createPoint.position);
            foreach (var overlay in overlaps)
            {
                if (overlay.CompareTag("LightBall")) return;
            }
            
            var theBall = Instantiate(lightBalls[Random.Range(0, lightBalls.Count)].transform).GetComponent<LightBall>();
            theBall.transform.position = createPoint.position;
            theBall.Init();
        }
    }
}