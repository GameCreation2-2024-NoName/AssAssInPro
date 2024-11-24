using System;
using Pditine.Audio;
using Pditine.GamePlay.GameManager;
using Pditine.GamePlay.UI;
using PurpleFlowerCore;
using TMPro;
using UnityEngine;

namespace Pditine.Map
{
    //DeadLine上也挂载了该脚本,id为0,用于处理球被打飞
    public class Goal : MonoBehaviour
    {
        [SerializeField] private SoccerScore scoreUI;
        [SerializeField] private int id;
        [SerializeField] private Transform createPoint;
        [SerializeField] private GameObject soccerPrototype;
        private int _score;

        private void Start()
        {
            if(id != 0)
            {
                scoreUI.SetScore(_score);
                int commandId = id == 1 ? 2 : 1;
                DebugSystem.AddCommand($"Soccer/Player{commandId}Score", (int a) => ChangeScore(a));
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("DynamicBarrier"))
            {

                var theBall = other.GetComponent<Soccer>();
                if(theBall.HasTriggered)return;
                ChangeScore(1);
                theBall.Destroy();
                CreateSoccer();
            }
        }

        private void ChangeScore(int delta)
        {
            if (id == 0)
            {
                return;
            }
            //AAIAudioManager.Instance.PlayEffect(cheerAudioName);
            
            _score += delta;
            if(_score < 0) _score = 0;
            scoreUI.SetScore(_score);

            SoccerGameManager.Instance.CheckGameOver(id,_score);
        }

        private void CreateSoccer()
        {
            //todo:对象池
            //var theBall = PoolSystem.GetGameObject(lightBalls[Random.Range(0, lightBalls.Count)]).GetComponent<LightBall>();
        
            // var overlaps = Physics2D.OverlapPointAll(createPoint.position);
            // foreach (var overlay in overlaps)
            // {
            //     if (overlay.CompareTag("Ass")) return;
            // }
            
            var theBall = Instantiate(soccerPrototype).GetComponent<Soccer>();
            theBall.transform.position = createPoint.position;
            theBall.Init();
        }
    }
}