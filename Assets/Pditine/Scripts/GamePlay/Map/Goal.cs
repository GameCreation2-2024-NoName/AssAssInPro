using System;
using System.Collections.Generic;
using Pditine.Collide;
using Pditine.GamePlay.GameManager;
using Pditine.GamePlay.LightBall;
using TMPro;
using UnityEngine;

namespace Pditine.Map
{
    public class Goal : MonoBehaviour
    {
        // [SerializeField] private Transform createPoint;
        // [SerializeField] private List<GameObject> lightBalls = new();
        [SerializeField] private TextMeshPro scoreUI;
        [SerializeField] private int id;
        //[SerializeField] private Wall theWall;
        private int _score;

        // private void Start()
        // {
        //     theWall.OnCollide += OnCollide;
        // }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("DynamicBarrier"))
                UpdateScore();
        }

        private void UpdateScore()
        {
            _score++;
            scoreUI.text = _score.ToString();
            SoccerGameManager.Instance.CheckGameOver(id,_score);
        }
        // private void CreateLightBall()
        // {
        //     //todo:对象池
        //     //var theBall = PoolSystem.GetGameObject(lightBalls[Random.Range(0, lightBalls.Count)]).GetComponent<LightBall>();
        //
        //     var overlaps = Physics2D.OverlapPointAll(createPoint.position);
        //     foreach (var overlay in overlaps)
        //     {
        //         if (overlay.CompareTag("LightBall")) return;
        //     }
        //     
        //     var theBall = Instantiate(lightBalls[Random.Range(0, lightBalls.Count)].transform).GetComponent<LightBall>();
        //     theBall.transform.position = createPoint.position;
        //     theBall.Init();
        // }
    }
}